using OpenExpenseApp.Models;

namespace OpenExpenseApp.Interfaces
{
    public interface IIncomeRepository : IRepository<Income>
    {
        Task<IEnumerable<Income>> GetIncomesByDateAsync(DateTime date);
        Task<IEnumerable<Income>> GetIncomesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalIncomesByDateAsync(DateTime date);
        Task<decimal> GetTotalIncomesByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
