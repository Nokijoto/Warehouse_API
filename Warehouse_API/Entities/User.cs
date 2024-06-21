using Common.Entities;
using Warehouse_API.Enums;

namespace Warehouse_API.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }
    }
}
