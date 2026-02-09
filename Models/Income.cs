namespace AccountingApp.Models
{
    public class Income
    {
        public int Id { get; set; }
        public string SourceName { get; set; } = string.Empty;
        public string IncomeType { get; set; } = string.Empty;
        public decimal GrossAmount { get; set; }
        public decimal? TaxWithheld { get; set; }
        public DateTime ReceivedDate { get; set; }
        public bool IsTaxable { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
