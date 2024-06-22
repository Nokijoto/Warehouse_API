using Common.Dto;
using Warehouse_API.Dto;
using Warehouse_API.Entities;

namespace Warehouse_API.Interfaces.IServices
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDTO>> GetProductsAsync();
        public Task<CrudOperationResult<ProductDTO>> GetProductById(int Id);
        public Task<CrudOperationResult<ProductDTO>> GetProductByGuid(Guid guid);
        public Task<CrudOperationResult<ProductDTO>> GetProductByRfidTag(string tag);
        public Task<CrudOperationResult<ProductDTO>> CreateProductAsync(ProductDTO product);

        public  Task<CrudOperationResult<ProductDTO>> Update(ProductDTO product);
        public Task<CrudOperationResult<ProductDTO>> Delete(int id);
    }
}
