using Microsoft.AspNetCore.Mvc;
using Warehouse_API.Dto;
using Warehouse_API.Interfaces.IServices;

namespace Warehouse_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : Controller
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ILogService _service;

        public LogsController(ILogger<WeatherForecastController> logger, ILogService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet(Name = "/all")]
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

        [HttpGet("/byDate", Name = "GetByDate")]
        public async Task<IActionResult> GetLogsByDate([FromBody] DateTime startDate,DateTime endDate)
        {
            var startLog = new LogsDto { CreatedAt= startDate};
            var endLog = new LogsDto { CreatedAt= endDate};

            var log = await _service.GetByDateRange(startLog,endLog);
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
