using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swap.Business.Abstract;
using Swap.Business.DTOs;
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
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all swap requests");
            var result = await _swapService.GetAll();
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Message);
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
            _logger.LogInformation($"Getting swap requests with status: {status}");
            var result = await _swapService.GetByStatus(status);
            return result.IsSuccess ? Ok(result.Data) : NotFound("No swap requests found");
        }

        [HttpPost]
        public async Task<IActionResult> CreateSwapRequest([FromBody] CreateSwapRequestDto dto)
        {
            var request = new SwapRequest
            {
                RequesterId = dto.RequesterId,
                RequestedBookId = dto.RequestedBookId,
                OfferedBookId = dto.OfferedBookId,
                Notes = dto.Notes,
                Status = SwapStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await _swapService.CreateSwapRequest(request);
            return result.IsSuccess ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpPut("{requestId}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStatus(int requestId, [FromBody] SwapStatus status)
        {
            _logger.LogInformation($"Updating status for swap request {requestId} to {status}");
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
