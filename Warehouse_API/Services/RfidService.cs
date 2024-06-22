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

        public RfidService(WarehouseDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<RFIDTagDTO>> GetAll()
        {
            return await _db.RFIDTags.Select(x => x.ToDto()).ToListAsync();
        }

        public async Task<CrudOperationResult<RFIDTagDTO>> GetByGuid(Guid guid)
        {
            try
            {
                var item = await _db.RFIDTags.SingleOrDefaultAsync(x => x.Guid == guid);
                if (item == null)
                {
                    return new CrudOperationResult<RFIDTagDTO>
                    {
                        Result = null,
                        Message = "Tag not found",
                        Status = CrudOperationResultStatus.RecordNotFound
                    };
                }
                return new CrudOperationResult<RFIDTagDTO>
                {
                    Result = item.ToDto(),
                    Status = CrudOperationResultStatus.Success,
                    Message = "Found Succesfully"
                };

            }
            catch (Exception ex)
            {
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
                var item = await _db.RFIDTags.FindAsync(Id);
                if (item == null)
                {
                    return new CrudOperationResult<RFIDTagDTO>
                    {
                        Result = null,
                        Message = "Product not found",
                        Status = CrudOperationResultStatus.RecordNotFound
                    };
                }
                return new CrudOperationResult<RFIDTagDTO>
                {
                    Result = item.ToDto(),
                    Status = CrudOperationResultStatus.Success,
                    Message = "Found Succesfully"
                };

            }
            catch (Exception ex)
            {
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
                var item = await _db.RFIDTags.FindAsync(product.Id);


                if (item == null)
                {
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
                return new CrudOperationResult<RFIDTagDTO>
                {
                    Result = product,
                    Status = CrudOperationResultStatus.Success,
                    Message = "Updated Succesfully"
                };
            }
            catch (Exception ex)
            {
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
                var item = await _db.RFIDTags.FindAsync(id);
                if (item == null)
                {
                    return new CrudOperationResult<RFIDTagDTO>
                    {
                        Result = null,
                        Message = "Product not found",
                        Status = CrudOperationResultStatus.RecordNotFound
                    };
                }
                _db.RFIDTags.Remove(item);
                await _db.SaveChangesAsync();
                return new CrudOperationResult<RFIDTagDTO>
                {
                    Result = item.ToDto(),
                    Status = CrudOperationResultStatus.Success,
                    Message = "Deleted Succesfully"
                };
            }
            catch (Exception ex)
            {
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
                await _db.RFIDTags.AddAsync(product.ToEntity());
                await _db.SaveChangesAsync();
                return new CrudOperationResult<RFIDTagDTO>
                {
                    Result = product,
                    Status = CrudOperationResultStatus.Success,
                    Message = "Created Succesfully"
                };

            }
            catch (Exception ex)
            {
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
