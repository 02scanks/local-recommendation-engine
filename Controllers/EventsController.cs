using LocalRecomendationEngine.Data;
using LocalRecomendationEngine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


        [HttpGet]
        public async Task<IActionResult> GetBrowsingEvents()
        {
            _logger.LogInformation("Retrieving browsing events.");
            var events = await _context.BrowsingEvents
                .OrderByDescending(e => e.Timestamp)
                .Take(50)
                .ToListAsync();

            _logger.LogInformation("Browsing events retrieved successfully.");
            return Ok(events);

        }
    }
}