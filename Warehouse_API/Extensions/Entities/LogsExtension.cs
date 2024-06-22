using Warehouse_API.Dto;
using Warehouse_API.Entities;

namespace Warehouse_API.Extensions.Entities
{
    public static class LogsExtension
    {
        public static LogsDto ToDto(this Logs item)
        {
            return new LogsDto
            {
                Id = item.Id,
                Guid = item.Guid,
                CreatedAt = item.CreatedAt,
                User = item.User,
                LogType = item.LogType,
                Message = item.Message
            };
        }
    }
}
