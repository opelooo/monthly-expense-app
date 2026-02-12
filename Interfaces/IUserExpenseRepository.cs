using OpenExpenseApp.Models;

namespace OpenExpenseApp.Interfaces
{
    public interface IUserExpenseRepository : IRepository<UserExpense>
    {
        Task<IEnumerable<UserExpense>> GetByUserIdAsync(string userId);
        Task<IEnumerable<UserExpense>> GetByExpenseIdAsync(string expenseId);

        // Perhatikan tanda kurung ganda: Task<(Tipe1, Tipe2)>
        Task<(IEnumerable<UserExpense> Items, int TotalCount)> GetByUserIdPaginatedAsync(
            string userId,
            int page,
            int pageSize
        );
    }
}
