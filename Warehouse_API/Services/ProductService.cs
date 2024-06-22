using Common.Dto;
using Common.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Warehouse_API.Dto;
using Warehouse_API.Entities;
using Warehouse_API.Extensions;
using Warehouse_API.Interfaces.IServices;

namespace Warehouse_API.Services
{
    public class ProductService : IProductService
    {
        private readonly WarehouseDbContext _db;

        public ProductService(WarehouseDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            return await _db.Products.Select(x=>x.ToDto()).ToListAsync();
        }


        public async Task<CrudOperationResult<ProductDTO>> CreateProductAsync(ProductDTO product)
        {
            try
            {
                await _db.Products.AddAsync(product.ToEntity());
                await _db.SaveChangesAsync();
                return new CrudOperationResult<ProductDTO>
                {
                    Result = product,
                    Status = CrudOperationResultStatus.Success,
                    Message = "Created Succesfully"
                };

            }
            catch (Exception ex)
            {
                return new CrudOperationResult<ProductDTO> 
                { 
                    Result = product,
                    Message =ex.Message , 
                    Status = CrudOperationResultStatus.Failure 
                };
            }
           
        }

        public async Task<CrudOperationResult<ProductDTO>> Update(ProductDTO product)
        {
            try
            {
                var item = await _db.Products.FindAsync(product.Id);


                if (item == null)
                {
                    return new CrudOperationResult<ProductDTO>
                    {
                        Result = product,
                        Message = "Product not found",
                        Status = CrudOperationResultStatus.RecordNotFound
                    };
                }
                item.Name = product.Name;
                item.Price = product.Price;
                item.Description = product.Description;
                item.Stock = product.Stock;
                item.UpdatedAt = DateTime.Now;
                item.UpdatedBy = "System";
                await _db.SaveChangesAsync();
                return new CrudOperationResult<ProductDTO>
                {
                    Result = product,
                    Status = CrudOperationResultStatus.Success,
                    Message = "Updated Succesfully"
                };
            }
            catch (Exception ex)
            {
                return new CrudOperationResult<ProductDTO>
                {
                    Result = product,
                    Message = ex.Message,
                    Status = CrudOperationResultStatus.Failure
                };
            }

        }


        public async Task<CrudOperationResult<ProductDTO>> Delete(int id)
        {
            try
            {
                var item = await _db.Products.FindAsync(id);
                if (item == null)
                {
                    return new CrudOperationResult<ProductDTO>
                    {
                        Result = null,
                        Message = "Product not found",
                        Status = CrudOperationResultStatus.RecordNotFound
                    };
                }
                _db.Products.Remove(item);
                await _db.SaveChangesAsync();
                return new CrudOperationResult<ProductDTO>
                {
                    Result = item.ToDto(),
                    Status = CrudOperationResultStatus.Success,
                    Message = "Deleted Succesfully"
                };
            }
            catch (Exception ex)
            {
                return new CrudOperationResult<ProductDTO>
                {
                    Result = null,
                    Message = ex.Message,
                    Status = CrudOperationResultStatus.Failure
                };
            }
        }

        public async Task<CrudOperationResult<ProductDTO>> GetProductById(int Id)
        {
            try
            {
                var item = await _db.Products.FindAsync(Id);
                if (item == null)
                {
                    return new CrudOperationResult<ProductDTO>
                    {
                        Result = null,
                        Message = "Product not found",
                        Status = CrudOperationResultStatus.RecordNotFound
                    };
                }
                return new CrudOperationResult<ProductDTO>
                {
                    Result = item.ToDto(),
                    Status = CrudOperationResultStatus.Success,
                    Message = "Found Succesfully"
                };

            }
            catch(Exception ex)
            {
                return new CrudOperationResult<ProductDTO>
                {
                    Result = null,
                    Message = ex.Message,
                    Status = CrudOperationResultStatus.Failure
                };
            }
        }

        public async Task<CrudOperationResult<ProductDTO>> GetProductByGuid(Guid guid)
        {
            try
            {
                var item = await _db.Products.SingleOrDefaultAsync(x => x.Guid == guid);
                if (item == null)
                {
                    return new CrudOperationResult<ProductDTO>
                    {
                        Result = null,
                        Message = "Product not found",
                        Status = CrudOperationResultStatus.RecordNotFound
                    };
                }
                return new CrudOperationResult<ProductDTO>
                {
                    Result = item.ToDto(),
                    Status = CrudOperationResultStatus.Success,
                    Message = "Found Succesfully"
                };

            }
            catch (Exception ex)
            {
                return new CrudOperationResult<ProductDTO>
                {
                    Result = null,
                    Message = ex.Message,
                    Status = CrudOperationResultStatus.Failure
                };
            }
        }

        public async Task<CrudOperationResult<ProductDTO>> GetProductByRfidTag(RFIDTag tag)
        {
            throw new NotImplementedException();
        }
    }
}
