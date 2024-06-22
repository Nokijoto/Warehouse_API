using Common.Dto;
using Common.Enums;
using Microsoft.EntityFrameworkCore;
using Warehouse_API.Dto;
using Warehouse_API.Entities;
using Warehouse_API.Extensions.Dtos;
using Warehouse_API.Extensions.Entities;
using Warehouse_API.Interfaces.IServices;

namespace Warehouse_API.Services
{
    public class LogService : ILogService
    {

        private readonly WarehouseDbContext _db;

        public LogService(WarehouseDbContext db)
        {
            _db = db;
        }
        public async Task Add(LogsDto log)
        {
            log.Guid = Guid.NewGuid();
            log.User = "NotSpecified";
            await _db.Logs.AddAsync(log.ToEntity());
            await _db.SaveChangesAsync();

        }
        public async Task<IEnumerable<LogsDto>> GetAll()
        {
            return await _db.Logs.Select(x => x.ToDto()).ToListAsync();
        }

        public async Task<IEnumerable<LogsDto>> GetByDateRange(LogsDto logStart, LogsDto logEnd)
        {
            return await _db.Logs.Where(x => x.CreatedAt >= logStart.CreatedAt && x.CreatedAt <= logEnd.CreatedAt).Select(x => x.ToDto()).ToListAsync();
        }
    }
}
