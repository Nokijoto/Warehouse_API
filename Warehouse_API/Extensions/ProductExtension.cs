using Warehouse_API.Dto;
using Warehouse_API.Entities;

namespace Warehouse_API.Extensions
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
                Stock = product.Stock,
                UpdatedAt = product.UpdatedAt,
                UpdatedBy = product.UpdatedBy,


            };
        }
    }
}
