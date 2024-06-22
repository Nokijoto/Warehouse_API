using Common.Dto;
using Warehouse_API.Dto;
using Warehouse_API.Entities;

namespace Warehouse_API.Interfaces.IServices
{
    public interface IRfidService
    {
        public Task<IEnumerable<RFIDTagDTO>> GetAll();
        public Task<CrudOperationResult<RFIDTagDTO>> GetById(int Id);
        public Task<CrudOperationResult<RFIDTagDTO>> GetByGuid(Guid guid);
        public Task<CrudOperationResult<RFIDTagDTO>> CreateAsync(RFIDTagDTO product);

        public Task<CrudOperationResult<RFIDTagDTO>> Update(RFIDTagDTO product);
        public Task<CrudOperationResult<RFIDTagDTO>> Delete(int id);
    }
}
