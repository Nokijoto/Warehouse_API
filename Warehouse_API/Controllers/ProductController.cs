using Common.Dto;
using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse_API.Dto;
using Warehouse_API.Dto.CreationsDto;
using Warehouse_API.Entities;
using Warehouse_API.Interfaces.IServices;
using Warehouse_API.Services;
using Newtonsoft.Json;

namespace Warehouse_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly ILogService _logService;
        public ProductController(ILogger<ProductController> logger,IProductService productService, ILogService logService)
        {
            _logger = logger;
            _productService = productService;
            _logService = logService;
        }

        [HttpGet(Name = "AllProducts"), ]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetProductsAsync();
            if (products == null)
            {
                _logService.Add(new LogsDto { LogType = "Error", Message = "Controller Products not found", CreatedAt = DateTime.Now });
                _logger.LogError("Products not found");
                return NotFound();
            }
            _logService.Add(new LogsDto { LogType= "Get", Message = "Controller Products found", CreatedAt = DateTime.Now });
            _logger.LogInformation("Products found");
            return Ok(products);
        }


        [HttpPost, ]
        public async Task<ActionResult<CrudOperationResult<ProductDTO>>> CreateProductAsync([FromBody] CreateProductDto createProductDto)
        {
            if (!ModelState.IsValid)
            {
                _logService.Add(new LogsDto { LogType = "Error", Message = "Controller Invalid model state", CreatedAt = DateTime.Now });
                _logger.LogError("Invalid model state");
                return BadRequest(ModelState);
            }
            var product = new ProductDTO
            {
                Name = createProductDto.Name,
                Price = createProductDto.Price,
                Description = createProductDto.Description,
                CreatedAt = DateTime.Now,
                CreatedBy = "System",
                Guid = Guid.NewGuid(),
                Stock = createProductDto.Stock,
                UpdatedAt = DateTime.Now,
                UpdatedBy = "System",
                Id = 0
            };

            _logService.Add(new LogsDto { LogType = "Create", Message = "Controller Product created", CreatedAt = DateTime.Now });
            _logger.LogInformation("Product created");
            return await _productService.CreateProductAsync(product);
        }

        [HttpPatch("{id}"),]
        public async Task<ActionResult<CrudOperationResult<ProductDTO>>> UpdateProductAsync([FromBody] CreateProductDto product,int id)
        {
            if (!ModelState.IsValid)
            {
                _logService.Add(new LogsDto { LogType = "Error", Message = "Controller Invalid model state", CreatedAt = DateTime.Now });
                _logger.LogError("Invalid model state");
                return BadRequest(ModelState);
            }
            var updatedProduct = new ProductDTO
            {
                CreatedAt = DateTime.Now,
                CreatedBy = "System",
                Description = product.Description,
                Guid = Guid.NewGuid(),
                Name = product.Name,
                Id= id,
                Price = product.Price,
                Stock = product.Stock,
                UpdatedAt = DateTime.Now,
                UpdatedBy = "System"
            };

            _logService.Add(new LogsDto { LogType = "Update", Message = "Controller Product updated", CreatedAt = DateTime.Now });
            _logger.LogInformation("Product updated");
            return await _productService.Update(updatedProduct);
        }

        [HttpDelete("{id}"),]
        public async Task<ActionResult<CrudOperationResult<ProductDTO>>> DeleteAsync(int id)
        {
            _logService.Add(new LogsDto { LogType = "Delete", Message = $"Controller Product {id}deleted", CreatedAt = DateTime.Now });
            _logger.LogInformation("Product deleted");
            return await _productService.Delete(id);
        }


        [HttpGet("{id}"),]
        public async Task<ActionResult<CrudOperationResult<ProductDTO>>> GetProductById(int id)
        {

            _logService.Add(new LogsDto { LogType = "Get", Message = "Controller Product found", CreatedAt = DateTime.Now });
            _logger.LogInformation("Product found");
             return await _productService.GetProductById(id);
        }

        [HttpGet("guid/{guid}"),]
        public async Task<ActionResult<CrudOperationResult<ProductDTO>>> GetProductByGuid(Guid guid)
        {
            _logService.Add(new LogsDto { LogType = "Get", Message = "Controller Product found", CreatedAt = DateTime.Now });
            _logger.LogInformation("Product found");
            return await _productService.GetProductByGuid(guid);
        }



        [HttpGet("rfid/{tag}"),]
        public async Task<ActionResult<CrudOperationResult<ProductDTO>>> GetProductByRfidTag(RFIDTag tag)
        {
            _logService.Add(new LogsDto { LogType = "Get", Message = "Controller Product found", CreatedAt = DateTime.Now });
            _logger.LogInformation("Product found");
            return await _productService.GetProductByRfidTag(tag);
        }  





    }
}
