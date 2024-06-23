using Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse_API.Entities
{
    [Table("Products", Schema = "Warehouse")]
    public class Product: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public int? RFIDTagId { get; set; }

        public RFIDTag RFIDTag { get; set; }
    }
}
