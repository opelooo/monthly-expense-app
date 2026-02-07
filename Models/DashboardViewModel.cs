namespace AccountingApp.Models
{
    public class DashboardViewModel
    {
        public decimal TotalExpenses { get; set; }
        public decimal TodayExpenses { get; set; }
        public decimal ThisMonthExpenses { get; set; }
        public decimal AvgPerDay { get; set; }
        public int TotalExpenseCount { get; set; }
        public IEnumerable<Expense> RecentExpenses { get; set; } = new List<Expense>();
        public Dictionary<string, decimal> ExpensesByPaymentMethod { get; set; } =
            new Dictionary<string, decimal>();
        public Dictionary<string, double> ExpensesByPaymentMethodPercent { get; set; } =
            new Dictionary<string, double>();
    }
}
