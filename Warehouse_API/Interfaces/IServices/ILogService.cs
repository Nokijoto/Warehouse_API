using Common.Dto;
using Warehouse_API.Dto;
using Warehouse_API.Extensions.Dtos;

namespace Warehouse_API.Interfaces.IServices
{
    public interface ILogService
    {
        public void Add(LogsDto log);
        public Task<IEnumerable<LogsDto>> GetAll();
        public Task<IEnumerable<LogsDto>> GetByDateRange(DateRange range);
    }

}
