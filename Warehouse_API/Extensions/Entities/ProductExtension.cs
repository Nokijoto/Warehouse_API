using Warehouse_API.Dto;
using Warehouse_API.Entities;

namespace Warehouse_API.Extensions.Entities
{
    public static class ProductExtension
    {
        public static ProductDTO ToDto(this Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                CreatedAt = product.CreatedAt,
                CreatedBy = product.CreatedBy,
                Description = product.Description,
                Guid = product.Guid,
                Price = product.Price,
                UpdatedAt = product.UpdatedAt,
                UpdatedBy = product.UpdatedBy,
            };
        }
    }
}
