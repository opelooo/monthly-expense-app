namespace AccountingApp.Models
{
    public class Income
    {
        public string Id { get; set; } = string.Empty; // ULID
        public string UserId { get; set; } = string.Empty;
        public string SourceName { get; set; } = string.Empty;
        public string IncomeType { get; set; } = string.Empty;
        public decimal GrossAmount { get; set; }
        public decimal? TaxWithheld { get; set; }
        public DateTime ReceivedDate { get; set; }
        public bool IsTaxable { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
