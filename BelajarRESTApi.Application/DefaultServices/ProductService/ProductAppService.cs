using AutoMapper;
using BelajarRESTApi.Application.DefaultServices.ProductService.Dto;
using BelajarRESTApi.Application.Helpers;
using BelajarRESTApi.Database.Databases;
using BelajarRESTApi.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelajarRESTApi.Application.DefaultServices.ProductService
{
    public class ProductAppService : IProductAppService
    {
        private readonly SalesContext _salesContext;
        private IMapper _mapper;

        public ProductAppService(SalesContext salesContext, IMapper mapper)
        {
            _salesContext = salesContext;
            _mapper = mapper;
        }

        public async Task<PagedResult<ProductListDto>> GetAllProducts(PageInfo pageinfo)
        {
            var pagedResult = new PagedResult<ProductListDto>
            {
                Data = (from product in _salesContext.Products
                        join supplier in _salesContext.Suppliers
                                 on product.SupplierId equals supplier.SupplierId
                        select new ProductListDto
                        {
                            ProductCode = product.ProductCode,
                            ProductName = product.ProductName,
                            ProductPrice = product.ProductPrice,
                            ProductQty = product.ProductQty,
                            SupplierName = supplier.SupplierName
                        })
                        .Skip(pageinfo.Skip)
                        .Take(pageinfo.PageSize)
                        .OrderBy(w => w.ProductCode),
                Total = _salesContext.Products.Count()
            };

            return await Task.Run(() => pagedResult);
        }

        public async Task<(bool, string)> Create(CreateProductDto model)
        {
            try
            {
                var product = _mapper.Map<Product>(model);

                await _salesContext.Database.BeginTransactionAsync();// Begin Transaction
                _salesContext.Products.Add(product);
                await _salesContext.SaveChangesAsync();

                await _salesContext.Database.CommitTransactionAsync();// Commit
                return await Task.Run(() => (true, "Success"));
            }
            catch (DbException dbex)
            {
                await _salesContext.Database.RollbackTransactionAsync();
                return await Task.Run(() => (false, dbex.Message));
            }
        }

        public async Task<(bool, string)> Delete(int id)
        {
            try
            {
                var product = await _salesContext.Products.FirstOrDefaultAsync(w => w.ProductId == id);

                if (product != null)
                {
                    await _salesContext.Database.BeginTransactionAsync();
                    _salesContext.Products.Remove(product);
                    await _salesContext.SaveChangesAsync();
                    await _salesContext.Database.CommitTransactionAsync();
                }
                return await Task.Run(() => (true, "Success"));
            }
            catch (DbException dbex)
            {
                await _salesContext.Database.RollbackTransactionAsync();
                return await Task.Run(() => (false, dbex.Message));
            }
        }

        public async Task<(bool, string)> Update(UpdateProductDto model)
        {
            try
            {
                var product = _mapper.Map<Product>(model);

                await _salesContext.Database.BeginTransactionAsync();
                _salesContext.Products.Update(product);
                await _salesContext.SaveChangesAsync();

                await _salesContext.Database.CommitTransactionAsync();
                return await Task.Run(() => (true, "Success"));
            }
            catch (DbException dbex)
            {
                await _salesContext.Database.RollbackTransactionAsync();
                return await Task.Run(() => (false, dbex.Message));
            }
        }

        public async Task<UpdateProductDto> GetProductByCode(string code)
        {
            var product = await _salesContext.Products.FirstOrDefaultAsync(w => w.ProductCode == code);
            var productDto = _mapper.Map<UpdateProductDto>(product);
            return productDto;
        }

        public async Task<PagedResult<ProductListDto>> SearchProduct(string searchString, PageInfo pageInfo)
        {
            var products = from product in _salesContext.Products
                           select product;

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.ProductName.Contains(searchString)
                                       || s.ProductCode.Contains(searchString));
            }

            var pagedResult = new PagedResult<ProductListDto>
            {
                Data = (from product in products
                        join supplier in _salesContext.Suppliers
                                 on product.SupplierId equals supplier.SupplierId
                        select new ProductListDto
                        {
                            ProductCode = product.ProductCode,
                            ProductName = product.ProductName,
                            ProductPrice = product.ProductPrice,
                            ProductQty = product.ProductQty,
                            SupplierName = supplier.SupplierName
                        })
                        .Skip(pageInfo.Skip)
                        .Take(pageInfo.PageSize)
                        .OrderBy(w => w.ProductCode),
                Total = _salesContext.Products.Count()
            };

            return await Task.Run(() => pagedResult);
        }
    }
}
