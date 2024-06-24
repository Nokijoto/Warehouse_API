using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Warehouse_API;
using Warehouse_API.Dto.CreationsDto;
using Warehouse_API.Dto;
using Warehouse_API.Entities;
using Warehouse_API.Interfaces.IServices;
using Common.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Warehouse_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    
    public class RFIDTagsController : Controller
    {
        private readonly ILogger<RFIDTagDTO> _logger;
        private readonly IRfidService _service;
        private readonly ILogService _logService;

        public RFIDTagsController(ILogger<RFIDTagDTO> logger, IRfidService service, ILogService logService)
        {
            _logger = logger;
            _service = service;
            _logService = logService;
        }

        [HttpGet(Name = "Alltags")]
        [Authorize(Policy = "AdminUserSystemPolicy")]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.GetAll();
            if (items == null)
            {
                _logService.Add(new LogsDto { LogType = "Error", Message = "Controller Tags not found", CreatedAt = DateTime.Now });
                _logger.LogError("Tags not found");
                return NotFound();
            }
            _logService.Add(new LogsDto { LogType = "Get", Message = "Controller Tags found", CreatedAt = DateTime.Now });
            _logger.LogInformation("Tags found");
            return Ok(items);
        }


        [HttpPost]
        [Authorize(Policy = "AdminUserSystemPolicy")]
        public async Task<ActionResult<CrudOperationResult<RFIDTagDTO>>> CreateAsync([FromBody] CreateRFIDDto item)
        {
            if (!ModelState.IsValid)
            {
                _logService.Add(new LogsDto { LogType = "Error", Message = "Controller Invalid model state", CreatedAt = DateTime.Now });
                _logger.LogError("Invalid model state");
                return BadRequest(ModelState);
            }
            var newItem = new RFIDTagDTO
            {
                TagNumber = item.TagNumber,
                CreatedAt = DateTime.Now,
                CreatedBy = "System",
                Guid = Guid.NewGuid(),
                UpdatedAt = DateTime.Now,
                UpdatedBy = "System",
                Id = 0
            };

            _logService.Add(new LogsDto { LogType = "Create", Message = "Controller Tag created", CreatedAt = DateTime.Now });
            _logger.LogInformation("Product created");
            return await _service.CreateAsync(newItem);
        }

        [HttpPatch("{id}")]
        [Authorize(Policy = "AdminUserSystemPolicy")]
        public async Task<ActionResult<CrudOperationResult<RFIDTagDTO>>> UpdateAsync([FromBody] CreateRFIDDto item, int id)
        {
            if (!ModelState.IsValid)
            {
                _logService.Add(new LogsDto { LogType = "Error", Message = "Controller Invalid model state", CreatedAt = DateTime.Now });
                _logger.LogError("Invalid model state");
                return BadRequest(ModelState);
            }
            var updatedItem = new RFIDTagDTO
            {
                TagNumber = item.TagNumber,
                CreatedAt = DateTime.Now,
                CreatedBy = "System",
                Guid = Guid.NewGuid(),
                Id = id,
                UpdatedAt = DateTime.Now,
                UpdatedBy = "System"
            };

            _logService.Add(new LogsDto { LogType = "Update", Message = "Controller Tag updated", CreatedAt = DateTime.Now });
            _logger.LogInformation("Tag updated");
            return await _service.Update(updatedItem);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminUserSystemPolicy")]
        public async Task<ActionResult<CrudOperationResult<RFIDTagDTO>>> DeleteAsync(int id)
        {
            _logService.Add(new LogsDto { LogType = "Delete", Message = "Controller Tag deleted", CreatedAt = DateTime.Now });
            _logger.LogInformation("Tag deleted");
            return await _service.Delete(id);
        }


        [HttpGet("{id}")]
        [Authorize(Policy = "AdminUserSystemPolicy")]
        public async Task<ActionResult<CrudOperationResult<RFIDTagDTO>>> GetById(int id)
        {
            _logService.Add(new LogsDto { LogType = "Get", Message = "Controller Tag found", CreatedAt = DateTime.Now });
            _logger.LogInformation("Tag found");
            return await _service.GetById(id);
        }

        [HttpGet("guid/{guid}")]
        [Authorize(Policy = "AdminUserSystemPolicy")]
        public async Task<ActionResult<CrudOperationResult<RFIDTagDTO>>> GetByGuid(Guid guid)
        {
            _logService.Add(new LogsDto { LogType = "Get", Message = "Controller Tag found", CreatedAt = DateTime.Now });
            _logger.LogInformation("Tag found");
            return await _service.GetByGuid(guid);
        }


        [HttpGet("randomTag")]
        [Authorize(Policy = "AdminUserSystemPolicy")]
        public async Task<ActionResult<CrudOperationResult<RFIDTagDTO>>> RandomTag()
        {
            _logService.Add(new LogsDto { LogType = "Get", Message = "Generated random Tag number", CreatedAt = DateTime.Now });
            _logger.LogInformation("Generating random tag");

            var random = new Random();
            var tagNumber = random.Next(100000, 999999).ToString(); 

            return new CrudOperationResult<RFIDTagDTO>
            {
                Result = new RFIDTagDTO
                {
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System",
                    Guid = Guid.NewGuid(),
                    TagNumber = tagNumber,
                    UpdatedAt = DateTime.Now,
                    Id = 0,
                    UpdatedBy = "System"
                },
                Status = CrudOperationResultStatus.Success,
                Message = "Tag found"
            };
        }

    }
}
