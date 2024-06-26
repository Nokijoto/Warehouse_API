﻿using Azure;
using Common.Dto;
using Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using Warehouse_API.Dto;
using Warehouse_API.Dto.CreationsDto;
using Warehouse_API.Entities;
using Warehouse_API.Extensions.Dtos;
using Warehouse_API.Extensions.Entities;
using Warehouse_API.Interfaces.IServices;

namespace Warehouse_API.Services
{
    public class ProductService : IProductService
    {
        private readonly WarehouseDbContext _db;
        private readonly ILogService _logService;

        public ProductService(WarehouseDbContext db, ILogService logService)
        {
            _db = db;
            _logService = logService;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            _logService.Add(new LogsDto { LogType = "Get", Message = "Get all products", CreatedAt = DateTime.Now });
            return await _db.Products.Select(x=>x.ToDto()).ToListAsync();
        }


        public async Task<CrudOperationResult<ProductDTO>> CreateProductAsync(ProductDTO product)
        {
            try
            {
                _logService.Equals(new LogsDto { LogType = "Create", Message = "Create product", CreatedAt = DateTime.Now });
                await _db.Products.AddAsync(product.ToEntity());
                await _db.SaveChangesAsync();
                var newItem = await _db.Products.SingleOrDefaultAsync(x => x.Guid == product.Guid);
               
                return new CrudOperationResult<ProductDTO>
                {
                    Result = newItem.ToDto(),
                    Status = CrudOperationResultStatus.Success,
                    Message = "Created Succesfully"
                };

            }
            catch (Exception ex)
            {
                _logService.Add(new LogsDto { LogType = "Error", Message = ex.Message, CreatedAt = DateTime.Now });
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
                _logService.Add(new LogsDto { LogType = "Update", Message = "Update product", CreatedAt = DateTime.Now });
                var item = await _db.Products.FindAsync(product.Id);


                if (item == null)
                {
                    _logService.Add(new LogsDto { LogType = "Error", Message = "Product not found", CreatedAt = DateTime.Now });    
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
                item.RFIDTagId = product.RFIDTagId;
                item.UpdatedAt = DateTime.Now;
                item.UpdatedBy = "System";
                await _db.SaveChangesAsync();
                var updatedItem = await _db.Products.FindAsync(product.Id);
                _logService.Add(new LogsDto { LogType = "Update", Message = "Product updated", CreatedAt = DateTime.Now });
                return new CrudOperationResult<ProductDTO>
                {
                    Result = updatedItem.ToDto(),
                    Status = CrudOperationResultStatus.Success,
                    Message = "Updated Succesfully"
                };
            }
            catch (Exception ex)
            {   
                _logService.Add(new LogsDto { LogType = "Error", Message = ex.Message, CreatedAt = DateTime.Now });
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
            {   _logService.Add(new LogsDto { LogType = "Delete", Message = "Delete product", CreatedAt = DateTime.Now });
                var item = await _db.Products.FindAsync(id);
                if (item == null)
                {
                    _logService.Add(new LogsDto { LogType = "Error", Message = "Product not found", CreatedAt = DateTime.Now });
                    return new CrudOperationResult<ProductDTO>
                    {
                        Result = null,
                        Message = "Product not found",
                        Status = CrudOperationResultStatus.RecordNotFound
                    };
                }
                _db.Products.Remove(item);
                await _db.SaveChangesAsync();
                _logService.Add(new LogsDto { LogType = "Delete", Message = "Product deleted", CreatedAt = DateTime.Now });
                return new CrudOperationResult<ProductDTO>
                {
                    Result = item.ToDto(),
                    Status = CrudOperationResultStatus.Success,
                    Message = "Deleted Succesfully"
                };
            }
            catch (Exception ex)
            {
                _logService.Add(new LogsDto { LogType = "Error", Message = ex.Message, CreatedAt = DateTime.Now });
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
                _logService.Add(new LogsDto { LogType = "Get", Message = "Get product by id", CreatedAt = DateTime.Now });
                var item = await _db.Products.FindAsync(Id);
                if (item == null)
                {
                    _logService.Add(new LogsDto { LogType = "Error", Message = "Product not found", CreatedAt = DateTime.Now });
                    return new CrudOperationResult<ProductDTO>
                    {
                        Result = null,
                        Message = "Product not found",
                        Status = CrudOperationResultStatus.RecordNotFound
                    };
                }
                _logService.Add(new LogsDto { LogType = "Get", Message = "Product found", CreatedAt = DateTime.Now });
                return new CrudOperationResult<ProductDTO>
                {
                    Result = item.ToDto(),
                    Status = CrudOperationResultStatus.Success,
                    Message = "Found Succesfully"
                };

            }
            catch(Exception ex)
            {
                _logService.Add(new LogsDto { LogType = "Error", Message = ex.Message, CreatedAt = DateTime.Now });
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
                _logService.Add(new LogsDto { LogType = "Get", Message = "Get product by guid", CreatedAt = DateTime.Now });
                var item = await _db.Products.SingleOrDefaultAsync(x => x.Guid == guid);
                if (item == null)
                {
                    _logService.Add(new LogsDto { LogType = "Error", Message = "Product not found", CreatedAt = DateTime.Now });
                    return new CrudOperationResult<ProductDTO>
                    {
                        Result = null,
                        Message = "Product not found",
                        Status = CrudOperationResultStatus.RecordNotFound
                    };
                }
                _logService.Add(new LogsDto { LogType = "Get", Message = "Product found", CreatedAt = DateTime.Now });
                return new CrudOperationResult<ProductDTO>
                {
                    Result = item.ToDto(),
                    Status = CrudOperationResultStatus.Success,
                    Message = "Found Succesfully"
                };

            }
            catch (Exception ex)
            {
                _logService.Add(new LogsDto { LogType = "Error", Message = ex.Message, CreatedAt = DateTime.Now });
                return new CrudOperationResult<ProductDTO>
                {
                    Result = null,
                    Message = ex.Message,
                    Status = CrudOperationResultStatus.Failure
                };
            }
        }

        public async Task<CrudOperationResult<ProductDTO>> GetProductByRfidTag(string tag)
        {

            try
            {
                _logService.Add(new LogsDto { LogType = "Get", Message = $"Attempting to get product by RFID tag: {tag}", CreatedAt = DateTime.Now });

                var tagItem = await _db.RFIDTags.SingleOrDefaultAsync(x => x.TagNumber == tag);
                if (tagItem == null)
                {
                    _logService.Add(new LogsDto { LogType = "Error", Message = $"RFID tag not found: {tag}", CreatedAt = DateTime.Now });
                    return new CrudOperationResult<ProductDTO>
                    {
                        Result = null,
                        Message = "RFID tag not found",
                        Status = CrudOperationResultStatus.RecordNotFound
                    };
                }

                var item = await _db.Products.SingleOrDefaultAsync(x => x.RFIDTagId == tagItem.Id);
                if (item == null)
                {
                    _logService.Add(new LogsDto { LogType = "Error", Message = $"Product not found for RFID tag: {tag}", CreatedAt = DateTime.Now });
                    return new CrudOperationResult<ProductDTO>
                    {
                        Result = null,
                        Message = "Product not found",
                        Status = CrudOperationResultStatus.RecordNotFound
                    };
                }

                _logService.Add(new LogsDto { LogType = "Get", Message = $"Product found for RFID tag: {tag}", CreatedAt = DateTime.Now });

                return new CrudOperationResult<ProductDTO>
                {
                    Result = item.ToDto(),
                    Status = CrudOperationResultStatus.Success,
                    Message = "Found successfully"
                };
            }
            catch (Exception ex)
            {
                _logService.Add(new LogsDto { LogType = "Error", Message = $"Exception occurred: {ex.Message}", CreatedAt = DateTime.Now });
                return new CrudOperationResult<ProductDTO>
                {
                    Result = null,
                    Message = ex.Message,
                    Status = CrudOperationResultStatus.Failure
                };
            }
        }

        public async Task<CrudOperationResult<IEnumerable<RaportDto>>> GetRaport()
        {
            try
            {
                _logService.Add(new LogsDto
                {
                    LogType = "Get",
                    Message = "Generate Raport",
                    CreatedAt = DateTime.Now
                });

                var items = await _db.Products.Select(x => x.ToRaportDto()).ToListAsync();

                return new CrudOperationResult<IEnumerable<RaportDto>>
                {
                    Result = items,
                    Status = CrudOperationResultStatus.Success,
                    Message = "Found successfully"
                };
            }
            catch (Exception ex)
            {
                _logService.Add(new LogsDto
                {
                    LogType = "Error",
                    Message = $"Exception occurred: {ex.Message}",
                    CreatedAt = DateTime.Now
                });

                return new CrudOperationResult<IEnumerable<RaportDto>>
                {
                    Result = null,
                    Message = ex.Message,
                    Status = CrudOperationResultStatus.Failure
                };
            }
        }
    }
}
