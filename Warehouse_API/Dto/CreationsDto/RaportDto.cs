using Microsoft.AspNetCore.Mvc;

namespace Warehouse_API.Dto.CreationsDto
{
    public class RaportDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
