using AccountingApp.Data;
using AccountingApp.Interfaces;
using AccountingApp.Models;

namespace AccountingApp.Repositories
{
    public class IncomeRepository : Repository<Income>, IIncomeRepository
    {
        public IncomeRepository(ApplicationDbContext dbContext)
            : base(dbContext) { }

        public async Task<IEnumerable<Income>> GetIncomesByDateAsync(DateTime date)
        {
            return await FindAsync(i => i.ReceivedDate.Date == date.Date);
        }

        public async Task<IEnumerable<Income>> GetIncomesByDateRangeAsync(
            DateTime startDate,
            DateTime endDate
        )
        {
            return await FindAsync(i =>
                i.ReceivedDate.Date >= startDate.Date && i.ReceivedDate.Date <= endDate.Date
            );
        }

        public async Task<decimal> GetTotalIncomesByDateAsync(DateTime date)
        {
            var incomes = await GetIncomesByDateAsync(date);
            return incomes.Sum(i => i.GrossAmount);
        }

        public async Task<decimal> GetTotalIncomesByDateRangeAsync(
            DateTime startDate,
            DateTime endDate
        )
        {
            var incomes = await GetIncomesByDateRangeAsync(startDate, endDate);
            return incomes.Sum(i => i.GrossAmount);
        }
    }
}
