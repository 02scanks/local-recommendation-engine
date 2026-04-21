namespace LocalRecomendationEngine.Models
{
    public class BrowsingEvent
    {
        public Guid Id { get; set; } = new Guid();
        public string Url { get; set; }
        public string Title { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}