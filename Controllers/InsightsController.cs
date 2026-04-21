using LocalRecomendationEngine.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocalRecomendationEngine.Controllers
{
    [ApiController]
    [Route("api/insights")]
    public class InsightsController(AppDbContext context, ILogger<InsightsController> logger) : ControllerBase
    {
        private readonly AppDbContext _context = context;
        private readonly ILogger<InsightsController> _logger = logger;

        [HttpGet("top-topics")]
        public async Task<IActionResult> GetTopTopics()
        {
            _logger.LogInformation("Retrieving top topics.");

            var events = await _context.BrowsingEvents
                .OrderByDescending(e => e.Timestamp)
                .Take(100)
                .ToListAsync();

            // Extract keywords from titles
            var keywords = new Dictionary<string, int>();

            foreach (var e in events)
            {
                if (string.IsNullOrWhiteSpace(e.Title)) continue;

                var words = e.Title
                    .ToLower()
                    .Split(new[] { ' ', '\t', '\n', '-', '|', ':', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries)
                    .Where(w => w.Length > 4) // Only words longer than 4 characters
                    .Select(w => System.Text.RegularExpressions.Regex.Replace(w, @"[^\w]", "")) // Remove punctuation
                    .Where(w => w.Length > 4);

                foreach (var word in words)
                {
                    if (keywords.ContainsKey(word))
                        keywords[word]++;
                    else
                        keywords[word] = 1;
                }
            }

            // Sort by count and take top 10
            var topTopics = keywords
                .OrderByDescending(k => k.Value)
                .Take(10)
                .Select(k => new { topic = k.Key, count = k.Value })
                .ToList();

            _logger.LogInformation("Top topics retrieved successfully.");
            return Ok(topTopics);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            _logger.LogInformation("Retrieving summary statistics.");

            var today = DateTime.UtcNow.Date;
            var events = await _context.BrowsingEvents.ToListAsync();

            // Total pages today
            var todayEvents = events.Where(e => e.Timestamp.Date == today).ToList();
            var totalToday = todayEvents.Count;

            // Top domain
            var domainCounts = new Dictionary<string, int>();
            foreach (var e in todayEvents)
            {
                var domain = ExtractDomain(e.Url);
                if (domainCounts.ContainsKey(domain))
                    domainCounts[domain]++;
                else
                    domainCounts[domain] = 1;
            }

            var topDomainEntry = domainCounts
                .OrderByDescending(d => d.Value)
                .FirstOrDefault();

            var topDomain = topDomainEntry.Key ?? "N/A";
            var topDomainCount = topDomainEntry.Value;

            // Peak hour
            var hourCounts = new Dictionary<int, int>();
            foreach (var e in todayEvents)
            {
                var hour = e.Timestamp.Hour;
                if (hourCounts.ContainsKey(hour))
                    hourCounts[hour]++;
                else
                    hourCounts[hour] = 1;
            }

            var peakHourEntry = hourCounts
                .OrderByDescending(h => h.Value)
                .FirstOrDefault();

            var peakHour = peakHourEntry.Key != 0
                ? DateTime.Today.AddHours(peakHourEntry.Key).ToString("h:00 tt")
                : "N/A";

            var summary = new
            {
                totalToday,
                topDomain,
                topDomainCount,
                peakHour
            };

            _logger.LogInformation("Summary statistics retrieved successfully.");
            return Ok(summary);
        }

        private static string ExtractDomain(string url)
        {
            try
            {
                var uri = new Uri(url);
                return uri.Host.Replace("www.", "");
            }
            catch
            {
                return "unknown";
            }
        }
    }
}
