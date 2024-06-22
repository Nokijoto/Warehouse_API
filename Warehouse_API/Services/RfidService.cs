using Common.Dto;
using Common.Enums;
using Microsoft.EntityFrameworkCore;
using Warehouse_API.Dto;
using Warehouse_API.Entities;
using Warehouse_API.Extensions.Dtos;
using Warehouse_API.Extensions.Entities;
using Warehouse_API.Interfaces;
using Warehouse_API.Interfaces.IServices;

namespace Warehouse_API.Services
{
    public class RfidService: IRfidService
    {
        private readonly WarehouseDbContext _db;
        private readonly ILogService _logService;

        public RfidService(WarehouseDbContext db, ILogService logService)
        {
            _db = db;
            _logService = logService;
        }

        public async Task<IEnumerable<RFIDTagDTO>> GetAll()
        {
            _logService.Add(new LogsDto { LogType = "Get", Message = "Get all tags", CreatedAt = DateTime.Now });
            return await _db.RFIDTags.Select(x => x.ToDto()).ToListAsync();
        }

        public async Task<CrudOperationResult<RFIDTagDTO>> GetByGuid(Guid guid)
        {
            try
            {
                _logService.Add(new LogsDto { LogType = "Get", Message = "Get tag by guid", CreatedAt = DateTime.Now });    
                var item = await _db.RFIDTags.SingleOrDefaultAsync(x => x.Guid == guid);
                if (item == null)
                {
                    _logService.Add(new LogsDto { LogType = "Error", Message = "Tag not found", CreatedAt = DateTime.Now });
                    return new CrudOperationResult<RFIDTagDTO>
                    {
                        Result = null,
                        Message = "Tag not found",
                        Status = CrudOperationResultStatus.RecordNotFound
                    };
                }
                _logService.Add(new LogsDto { LogType = "Get", Message = "Tag found", CreatedAt = DateTime.Now });
                return new CrudOperationResult<RFIDTagDTO>
                {
                    Result = item.ToDto(),
                    Status = CrudOperationResultStatus.Success,
                    Message = "Found Succesfully"
                };

            }
            catch (Exception ex)
            {
                _logService.Add(new LogsDto { LogType = "Error", Message = ex.Message, CreatedAt = DateTime.Now });
                return new CrudOperationResult<RFIDTagDTO>
                {
                    Result = null,
                    Message = ex.Message,
                    Status = CrudOperationResultStatus.Failure
                };
            }
        }

       
        public async Task<CrudOperationResult<RFIDTagDTO>> GetById(int Id)
        {
            try
            {
                _logService.Add(new LogsDto { LogType = "Get", Message = "Get tag by id", CreatedAt = DateTime.Now });
                var item = await _db.RFIDTags.FindAsync(Id);
                if (item == null)
                {
                    _logService.Add(new LogsDto { LogType = "Error", Message = "Product not found", CreatedAt = DateTime.Now });
                    return new CrudOperationResult<RFIDTagDTO>
                    {
                        Result = null,
                        Message = "Product not found",
                        Status = CrudOperationResultStatus.RecordNotFound
                    };
                }
                _logService.Add(new LogsDto { LogType = "Get", Message = "Product found", CreatedAt = DateTime.Now });
                return new CrudOperationResult<RFIDTagDTO>
                {
                    Result = item.ToDto(),
                    Status = CrudOperationResultStatus.Success,
                    Message = "Found Succesfully"
                };

            }
            catch (Exception ex)
            {
                _logService.Add(new LogsDto { LogType = "Error", Message = ex.Message, CreatedAt = DateTime.Now });
                return new CrudOperationResult<RFIDTagDTO>
                {
                    Result = null,
                    Message = ex.Message,
                    Status = CrudOperationResultStatus.Failure
                };
            }
        }

        public async Task<CrudOperationResult<RFIDTagDTO>> Update(RFIDTagDTO product)
        {
            try
            {
                _logService.Add(new LogsDto { LogType = "Update", Message = "Update tag", CreatedAt = DateTime.Now });
                var item = await _db.RFIDTags.FindAsync(product.Id);


                if (item == null)
                {

                    _logService.Add(new LogsDto { LogType = "Error", Message = "Product not found", CreatedAt = DateTime.Now });
                    return new CrudOperationResult<RFIDTagDTO>
                    {
                        Result = product,
                        Message = "Product not found",
                        Status = CrudOperationResultStatus.RecordNotFound
                    };
                }
                item.TagNumber = product.TagNumber;
                item.UpdatedAt = DateTime.Now;
                item.UpdatedBy = "System";
                await _db.SaveChangesAsync();
                var updatedItem = await _db.RFIDTags.FindAsync(product.Id);
                _logService.Add(new LogsDto { LogType = "Update", Message = "Tag updated", CreatedAt = DateTime.Now }); 
                return new CrudOperationResult<RFIDTagDTO>
                {
                    Result = updatedItem.ToDto(),
                    Status = CrudOperationResultStatus.Success,
                    Message = "Updated Succesfully"
                };
            }
            catch (Exception ex)
            {
                _logService.Add(new LogsDto { LogType = "Error", Message = ex.Message, CreatedAt = DateTime.Now });
                return new CrudOperationResult<RFIDTagDTO>
                {
                    Result = product,
                    Message = ex.Message,
                    Status = CrudOperationResultStatus.Failure
                };
            }

        }


        public async Task<CrudOperationResult<RFIDTagDTO>> Delete(int id)
        {
            try
            {
                _logService.Add(new LogsDto { LogType = "Delete", Message = "Delete tag", CreatedAt = DateTime.Now });  
                var item = await _db.RFIDTags.FindAsync(id);
                if (item == null)
                {

                    _logService.Add(new LogsDto { LogType = "Error", Message = "Product not found", CreatedAt = DateTime.Now });    
                    return new CrudOperationResult<RFIDTagDTO>
                    {
                        Result = null,
                        Message = "Product not found",
                        Status = CrudOperationResultStatus.RecordNotFound
                    };
                }
                _db.RFIDTags.Remove(item);
                await _db.SaveChangesAsync();

                _logService.Add(new LogsDto { LogType = "Delete", Message = "Tag deleted", CreatedAt = DateTime.Now });
                return new CrudOperationResult<RFIDTagDTO>
                {
                    Result = item.ToDto(),
                    Status = CrudOperationResultStatus.Success,
                    Message = "Deleted Succesfully"
                };
            }
            catch (Exception ex)
            {   
                _logService.Add(new LogsDto { LogType = "Error", Message = ex.Message, CreatedAt = DateTime.Now });
                return new CrudOperationResult<RFIDTagDTO>
                {
                    Result = null,
                    Message = ex.Message,
                    Status = CrudOperationResultStatus.Failure
                };
            }
        }

        


        

        public async Task<CrudOperationResult<RFIDTagDTO>> CreateAsync(RFIDTagDTO product)
        {
            try
            {
                _logService.Add(new LogsDto { LogType = "Create", Message = "Create tag", CreatedAt = DateTime.Now });
                await _db.RFIDTags.AddAsync(product.ToEntity());
                await _db.SaveChangesAsync();
                var newItem = await _db.RFIDTags.FirstOrDefaultAsync(x => x.Guid == product.Guid);
                _logService.Add(new LogsDto { LogType = "Create", Message = "Tag created", CreatedAt = DateTime.Now });
                return new CrudOperationResult<RFIDTagDTO>
                {
                    Result = newItem.ToDto(),
                    Status = CrudOperationResultStatus.Success,
                    Message = "Created Succesfully"
                };

            }
            catch (Exception ex)
            {
                _logService.Add(new LogsDto { LogType = "Error", Message = ex.Message, CreatedAt = DateTime.Now });
                return new CrudOperationResult<RFIDTagDTO>
                {
                    Result = product,
                    Message = ex.Message,
                    Status = CrudOperationResultStatus.Failure
                };
            }
        }

    }
}
