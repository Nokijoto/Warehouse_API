using Warehouse_API.Dto;
using Warehouse_API.Entities;

namespace Warehouse_API.Extensions.Dtos
{
    public static class RfidDtoExtension
    {
        public static RFIDTag ToEntity(this RFIDTagDTO item)
        {
            return new RFIDTag
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
