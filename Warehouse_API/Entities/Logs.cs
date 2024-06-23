using Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace Warehouse_API.Entities
{
    [Table("Logs", Schema = "Warehouse")]
    public class Logs
    {
        public string User { get; set; }
        public string Message { get; set; }
        public string LogType { get; set; }
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
