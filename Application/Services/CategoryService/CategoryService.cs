using Application.UseCases.Category;
using Application.UseCases.Expense;
using AutoMapper;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> GetCategory(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid category ID.", nameof(id));

            var category = await _categoryRepository.GetCategory(id);

            if (category == null)
                throw new Exception($"Category with ID {id} not found");

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> GetCategoryByUserId(int id, int userId)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid category ID.", nameof(id));

            var category = await _categoryRepository.GetCategoryByUserId(id, userId);

            if (category == null)
                throw new Exception($"Category with ID {id} not found");

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<List<CategoryDto>> GetCategoriesByUserId(int userId)
        {
            var categories = await _categoryRepository.GetCategoriesByUserId(userId);

            if (categories == null || categories.Count <= 0)
                throw new Exception("Categories not found");

            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> AddCategory(AddCategoryDto addCategoryDto, int userId)
        {
            if (addCategoryDto == null)
                throw new ArgumentNullException(nameof(addCategoryDto), "Category cannot be null.");

            var addedCategory = await _categoryRepository.AddCategory(_mapper.Map<Category>(addCategoryDto), userId);

            return _mapper.Map<CategoryDto>(addedCategory);
        }

        public async Task<bool> DeleteCategory(int id, int userId)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid category ID.", nameof(id));

            var category = await GetCategory(id);

            if (category == null)
                throw new Exception("Category not found");

            if (category.UserId != userId)
                throw new Exception("Access denied. You are not the authorized.");

            await _categoryRepository.DeleteCategory(id);
            return true;
        }

        public async Task<CategoryDto> UpdateCategory(UpdateCategoryDto updateCategoryDto, int id, int userId)
        {
            var category = await GetCategory(id);

            if (category.UserId == userId)
            {
                if (updateCategoryDto == null)
                    throw new ArgumentNullException(nameof(updateCategoryDto), "Category cannot be null.");

                var updatedCategory = await _categoryRepository.UpdateCategory(_mapper.Map<Category>(updateCategoryDto), id);

                return _mapper.Map<CategoryDto>(updatedCategory);
            }
            throw new Exception("Access denied. You must be authorized to edit a category.");
        }
    }
}
