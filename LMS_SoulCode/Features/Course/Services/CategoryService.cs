using LMS_SoulCode.Features.Course.Entities;
using LMS_SoulCode.Features.Course.Repositories;

namespace LMS_SoulCode.Features.Course.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category> CreateAsync(string name);
        Task UpdateAsync(int id, string newName);
        Task DeleteAsync(int id);
    }
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _category;

        public CategoryService(ICategoryRepository cateogryRepo)
            => _category = cateogryRepo;


        public async Task<IEnumerable<Category>> GetAllAsync()
            => await _category.GetAllAsync();

        public async Task<Category?> GetByIdAsync(int id)
            => await _category.GetByIdAsync(id);

        public async Task<Category> CreateAsync(string name)
        {
            var category = new Category { CategoryName = name };
            await _category.AddAsync(category);
            return category;
        }

        public async Task UpdateAsync(int id, string newName)
        {
            var category = await _category.GetByIdAsync(id);
            if (category == null)
                throw new Exception("Category not found");

            category.CategoryName = newName;
            await _category.UpdateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _category.GetByIdAsync(id);
            if (category == null)
                throw new Exception("Category not found");

            await _category.DeleteAsync(category);
        }
    }
}
