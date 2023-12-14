using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IExpenseRepository
    {
        Task<Expense> AddExpense(Expense expense, int userId);
        Task<List<Expense>> GetExpensesByUserId(int userId);
        Task<Expense> GetExpenseByUserId(int id, int userId);
        Task<Expense> GetExpense(int id);
        Task<Expense> UpdateExpense(Expense expense, int id);
        Task<bool> DeleteExpense(int id);
        //Task<IEnumerable<Expense>> GetExpensesByCategory(int userId, int categoryId);
        //Task<IEnumerable<Expense>> GetExpensesInDateRange(int userId, DateTime startDate, DateTime endDate);
    }
}
