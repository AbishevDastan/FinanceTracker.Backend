using Application.UseCases.Category;
using Application.UseCases.Expense;

namespace Application.Services.ExpenseService;

public interface IExpenseService
{
    Task<CategoryDto> AddExpense(AddCategoryDto expense, int userId);
    Task<List<CategoryDto>> GetExpensesByUserId(int userId);
    Task<CategoryDto> GetExpenseByUserId(int id, int userId);
    Task<CategoryDto> GetExpense(int id);
    Task<CategoryDto> UpdateExpense(UpdateCategoryDto expense, int id, int userId);
    Task<bool> DeleteExpense(int id, int userId);
}
