using Application.UseCases.Expense;

namespace Application.Services.ExpenseService;

public interface IExpenseService
{
    Task<ExpenseDto> AddExpense(AddExpenseDto expense, int userId);
    Task<List<ExpenseDto>> GetExpensesByUserId(int userId);
    Task<ExpenseDto> GetExpenseByUserId(int id, int userId);
    Task<ExpenseDto> GetExpense(int id);
    Task<ExpenseDto> UpdateExpense(UpdateExpenseDto expense, int id, int userId);
    Task<bool> DeleteExpense(int id, int userId);
}
