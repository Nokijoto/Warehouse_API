using Warehouse_API.Dto;
using Warehouse_API.Entities;

namespace Warehouse_API.Extensions.Entities
{
    public static class RfidExtension
    {
        public static RFIDTagDTO ToEntity(this RFIDTag product)
        {
            return new RFIDTagDTO
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
