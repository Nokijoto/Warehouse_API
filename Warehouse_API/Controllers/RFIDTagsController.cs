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

namespace Warehouse_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RFIDTagsController : Controller
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IRfidService _service;

        public RFIDTagsController(ILogger<WeatherForecastController> logger, IRfidService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet(Name = "Alltags"),]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.GetAll();
            if (items == null)
            {
                _logger.LogError("Tags not found");
                return NotFound();
            }
            _logger.LogInformation("Tags found");
            return Ok(items);
        }


        [HttpPost,]
        public async Task<ActionResult<CrudOperationResult<RFIDTagDTO>>> CreateAsync([FromBody] CreateRFIDDto item)
        {
            if (!ModelState.IsValid)
            {
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

            _logger.LogInformation("Product created");
            return await _service.CreateAsync(newItem);
        }

        [HttpPatch("{id}"),]
        public async Task<ActionResult<CrudOperationResult<RFIDTagDTO>>> UpdateAsync([FromBody] CreateRFIDDto item, int id)
        {
            if (!ModelState.IsValid)
            {
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
            _logger.LogInformation("Tag updated");
            return await _service.Update(updatedItem);
        }

        [HttpDelete("{id}"),]
        public async Task<ActionResult<CrudOperationResult<RFIDTagDTO>>> DeleteAsync(int id)
        {
            _logger.LogInformation("Tag deleted");
            return await _service.Delete(id);
        }


        [HttpGet("{id}"),]
        public async Task<ActionResult<CrudOperationResult<RFIDTagDTO>>> GetProductById(int id)
        {

            _logger.LogInformation("Tag found");
            return await _service.GetById(id);
        }

        [HttpGet("guid/{guid}"),]
        public async Task<ActionResult<CrudOperationResult<RFIDTagDTO>>> GetProductByGuid(Guid guid)
        {
            _logger.LogInformation("Tag found");
            return await _service.GetByGuid(guid);
        }


        [HttpGet("randomTag"),]
        public async Task<ActionResult<CrudOperationResult<RFIDTagDTO>>> RandomTag()
        {
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
