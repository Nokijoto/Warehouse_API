using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse_API.Dto;
using Warehouse_API.Interfaces.IServices;

namespace Warehouse_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : Controller
    {
        private readonly ILogger<LogsController> _logger;
        private readonly ILogService _service;

        public LogsController(ILogger<LogsController> logger, ILogService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet(Name = "/all")]
        [Authorize(Policy = "AdminHRSystemPolicy")]
        public async Task<IActionResult> GetLogs()
        {
            var logs = await _service.GetAll();
            if (logs == null)
            {
                _logger.LogError("Logs not found");
                return NotFound();
            }
            _logger.LogInformation("Logs found");
            return Ok(logs);
        }

        [HttpPatch("/byDate", Name = "GetByDate")]
        [Authorize(Policy= "AdminHRSystemPolicy")]
        public async Task<IActionResult> GetLogsByDate([FromBody] DateRange range)
        {

            var log = await _service.GetByDateRange(range);
            if (log == null)
            {
                _logger.LogError("Log not found");
                return NotFound();
            }
            _logger.LogInformation("Log found");
            return Ok(log);
        }
    }
}
