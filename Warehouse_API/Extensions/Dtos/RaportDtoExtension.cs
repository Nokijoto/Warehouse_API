using Microsoft.AspNetCore.Mvc;
using Warehouse_API.Dto;
using Warehouse_API.Dto.CreationsDto;
using Warehouse_API.Entities;

namespace Warehouse_API.Extensions.Dtos
{
    public static class RaportDtoExtension
    {
        public static Product ToEntity(this RaportDto product)
        {
            return new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }
    }
      
}
