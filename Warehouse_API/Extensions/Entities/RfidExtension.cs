using Warehouse_API.Dto;
using Warehouse_API.Entities;

namespace Warehouse_API.Extensions.Entities
{
    public static class RFIDTagExtension
    {
        public static RFIDTagDTO ToDto(this RFIDTag item)
        {
            return new RFIDTagDTO
            {
                Id = item.Id,
                CreatedAt = item.CreatedAt,
                CreatedBy = item.CreatedBy,
                Guid = item.Guid,
                TagNumber = item.TagNumber,
                UpdatedAt = item.UpdatedAt,
                UpdatedBy = item.UpdatedBy,
            };
        }
    }
}
