using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swap.Business.Abstract;
using Swap.Business.DTOs;
using Swap.DataAccess.EntityFramework;
using Swap.Entity.Concrete;
using Swap.Entity.Enums;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class SwapController : ControllerBase
    {
        private readonly ISwapService _swapService;
        private readonly ILogger<SwapController> _logger;

        public SwapController(ISwapService swapService, ILogger<SwapController> logger)
        {
            _swapService = swapService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all swap requests");

            try
            {
                var result = await _swapService.GetAll();

                _logger.LogInformation($"GetAll result: {result}");

                // Verinin null olup olmadığını kontrol et
                if (result == null || !result.IsSuccess)
                {
                    _logger.LogError("Swap service returned null or failed.");
                    return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
                }

                if (result.Data == null || result.Data.Count == 0)
                {
                    _logger.LogWarning("No swap requests found.");
                    return Ok(new List<SwapRequestDto>());
                }

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
            
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"Getting swap request by id: {id}");
            var result = await _swapService.GetById(id);
            return result.IsSuccess ? Ok(result.Data) : NotFound("Swap request not found");
        }

        [HttpGet("user/{requesterId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByRequesterId(int requesterId)
        {
            _logger.LogInformation($"Getting swap requests for user: {requesterId}");
            var result = await _swapService.GetByRequesterId(requesterId);
            return result.IsSuccess ? Ok(result.Data) : NotFound("No swap requests found");
        }

        [HttpGet("status/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByStatus(SwapStatus status)
        {
            _logger.LogInformation("Getting all swap requests");

            try
            {
                var result = await _swapService.GetAll();
                _logger.LogInformation($"GetAll result: {result}");

                if (result == null || !result.IsSuccess)
                {
                    _logger.LogError("Swap service returned null or failed.");
                    return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
                }

                if (result.Data == null || result.Data.Count == 0)
                {
                    _logger.LogWarning("No swap requests found.");
                    return Ok(new List<SwapRequestDto>());
                }

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

        [HttpPut("{requestId}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        public async Task<IActionResult> UpdateStatus(int requestId, [FromBody] SwapStatus status)
        {
            _logger.LogInformation($"Updating status for swap request {requestId} to {status}");
            if (!Enum.IsDefined(typeof(SwapStatus), status))
            {
            return BadRequest("Invalid status value");
            }
            var result = await _swapService.UpdateSwapStatus(requestId, status);
            return result.IsSuccess ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpGet("error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogError("An error occurred in SwapController");
            return BadRequest("An error occurred!");
        }
    }
}
