namespace BaglantiApp.Models
{
    public class Startup
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Sector { get; set; } = string.Empty;
        public string Stage { get; set; } = string.Empty;
        public decimal FundingGoal { get; set; }
        public decimal Raised { get; set; } = 0;
        public decimal Mrr { get; set; } = 0;
        public string Founder { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? University { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}