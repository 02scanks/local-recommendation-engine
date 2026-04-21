using LocalRecomendationEngine.Data;
using LocalRecomendationEngine.Models;
using Microsoft.AspNetCore.Mvc;

namespace LocalRecomendationEngine.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController(AppDbContext context, ILogger<EventsController> logger) : ControllerBase
    {
        private readonly AppDbContext _context = context;
        private readonly ILogger<EventsController> _logger = logger;


        [HttpPost("browsing")]
        public async Task<IActionResult> CreateBrowsingEvent([FromBody] BrowsingEvent browsingEvent)
        {
            _logger.LogInformation("Creating browsing event.");
            if (browsingEvent == null || string.IsNullOrWhiteSpace(browsingEvent.Url))
            {
                _logger.LogWarning("Invalid browsing event data.");
                return BadRequest("Invalid browsing event data.");
            }


            _context.BrowsingEvents.Add(browsingEvent);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Browsing event created successfully.");
            return Ok();

        }

    }
}