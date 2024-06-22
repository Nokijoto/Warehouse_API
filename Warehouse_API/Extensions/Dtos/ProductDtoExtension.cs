using Warehouse_API.Dto;
using Warehouse_API.Entities;

namespace Warehouse_API.Extensions.Dtos
{
    public static class ProductDtoExtension
    {
        public static Product ToEntity(this ProductDTO product)
        {
            return new Product
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
                UpdatedBy = product.UpdatedBy
            };
        }
    }
}
