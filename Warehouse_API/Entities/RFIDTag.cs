using Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse_API.Entities
{
    [Table("RFIDTag", Schema = "Warehouse")]
    public class RFIDTag :BaseEntity
    {
        public string TagNumber { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
    }
}
