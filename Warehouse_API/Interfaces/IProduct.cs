using Common.Interfaces;

namespace Warehouse_API.Interfaces
{
    public interface IProduct : IBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
