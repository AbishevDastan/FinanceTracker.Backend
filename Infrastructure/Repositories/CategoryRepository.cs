using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category> GetCategoryByUserId(int id, int userId)
        {
            return await _dbContext.Categories
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
        }

        public async Task<Category> GetCategory(int id)
        {
            return await _dbContext.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Category>> GetCategoriesByUserId(int userId)
        {
            return await _dbContext.Categories
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<Category> AddCategory(Category category, int userId)
        {
            var newCategory = new Category
            {
                Name = category.Name,
                UserId = userId
            };

            await _dbContext.Categories.AddAsync(newCategory);
            await _dbContext.SaveChangesAsync();

            return newCategory;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            _dbContext.Categories.Remove(await GetCategory(id));
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Category> UpdateCategory(Category category, int id)
        {
            var dbCategory = await GetCategory(id);

            if (dbCategory != null)
            {
                dbCategory.Name = category.Name;

                await _dbContext.SaveChangesAsync();

                return dbCategory;
            }
            throw new ArgumentNullException(nameof(category));
        }
    }
}

