using Common.Dto;

namespace Warehouse_API.Dto
{
    public class ProductDTO: BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int? RFIDTagId { get; set; }
        
    }
}
