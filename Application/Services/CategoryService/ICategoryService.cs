using Application.UseCases.Category;
using Application.UseCases.Expense;
using Domain.Entities;

namespace Application.Services.CategoryService;

public interface ICategoryService
{
    Task<CategoryDto> AddCategory(AddCategoryDto addCategoryDto, int userId);
    Task<List<CategoryDto>> GetCategoriesByUserId(int userId);
    Task<CategoryDto> GetCategoryByUserId(int id, int userId);
    Task<CategoryDto> GetCategory(int id);
    Task<CategoryDto> UpdateCategory(UpdateCategoryDto updateCategoryDto, int id, int userId);
    Task<bool> DeleteCategory(int id, int userId);
}
