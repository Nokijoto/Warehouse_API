using Warehouse_API.Dto;
using Warehouse_API.Entities;

namespace Warehouse_API.Extensions.Dtos
{
    public static class RfidDtoExtension
    {
        public static RFIDTag ToEntity(this RFIDTagDTO product)
        {
            return new RFIDTag
            {
                Id = product.Id,
                CreatedAt = product.CreatedAt,
                CreatedBy = product.CreatedBy,
                Guid = product.Guid,
                TagNumber = product.TagNumber,
                UpdatedAt = product.UpdatedAt,
                UpdatedBy = product.UpdatedBy,
            };
        }
    }
}
