namespace BaglantiApp.Models
{
    public class Investor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Focus { get; set; } = string.Empty;
        public decimal TotalInvested { get; set; } = 0;
        public decimal TotalReturned { get; set; } = 0;
        public int ActiveCount { get; set; } = 0;
        public int ExitCount { get; set; } = 0;
    }
}