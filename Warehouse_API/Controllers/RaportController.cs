using Common.Dto;
using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse_API.Dto.CreationsDto;
using Warehouse_API.Interfaces.IServices;

namespace Warehouse_API.Controllers
{
    public class RaportController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly ILogService _logService;
        public RaportController(ILogger<ProductController> logger, IProductService productService, ILogService logService)
        {
            _logger = logger;
            _productService = productService;
            _logService = logService;
        }


        [HttpGet("inventory")]
        [Authorize(Policy = "SystemPolicy")]
        [Authorize(Policy = "UserPolicy")]
        [Authorize(Policy = "AdminPolicy")]
        [Authorize(Policy = "HRPolicy")]
        public async Task<ActionResult<CrudOperationResult<IEnumerable<RaportDto>>>> GetInventoryReport()
        {
            var report = await _productService.GetRaport();

            if (report.Status == CrudOperationResultStatus.Success)
            {
                return Ok(report);
            }
            else
            {
                return BadRequest(report);
            }
        }
    }

}
