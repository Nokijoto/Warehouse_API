using Warehouse_API.Dto;
using Warehouse_API.Entities;

namespace Warehouse_API.Extensions.Dtos
{
    public static class LogsDtoExtension
    {
        public static Logs ToEntity(this LogsDto item)
        {
            return new Logs
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
