using Domain.Entities;

namespace Domain.Abstractions
{
    public interface ICategoryRepository
    {
        Task<Category> AddCategory(Category category, int userId);
        Task<List<Category>> GetCategoriesByUserId(int userId);
        Task<Category> GetCategoryByUserId(int id, int userId);
        Task<Category> GetCategory(int id);
        Task<Category> UpdateCategory(Category category, int id);
        Task<bool> DeleteCategory(int id);
    }
}
