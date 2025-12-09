using LMS_SoulCode.Features.Course.Models;
using CategoryEntity = LMS_SoulCode.Features.Course.Models.Category;
using LMS_SoulCode.Features.Course.Repositories;
using LMS_SoulCode.Features.Common;
using StatusCodes = LMS_SoulCode.Features.Common.StatusCodes;

namespace LMS_SoulCode.Features.Course.Services
{
    public interface ICategoryService
    {
        Task<ApiResponse<IEnumerable<CategoryEntity>>> GetAllAsync();
        Task<ApiResponse<CategoryEntity?>> GetByIdAsync(int id);
        Task<ApiResponse<CategoryEntity>> CreateAsync(string name);
        Task<ApiResponse<string>> UpdateAsync(int id, string newName);
        Task<ApiResponse<string>> DeleteAsync(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _category;

        public CategoryService(ICategoryRepository categoryRepo)
        {
            _category = categoryRepo;
        }

        public async Task<ApiResponse<IEnumerable<CategoryEntity>>> GetAllAsync()
        {
            var data = await _category.GetAllAsync();
            return ApiResponse<IEnumerable<CategoryEntity>>.Success(data, Messages.Success);
        }
        public async Task<ApiResponse<CategoryEntity?>> GetByIdAsync(int id)
        {
            var category = await _category.GetByIdAsync(id);

            if (category == null)
                return ApiResponse<CategoryEntity?>.Fail(Messages.NotFound, StatusCodes.NotFound);

            return ApiResponse<CategoryEntity?>.Success(category, Messages.Success);
        }
        public async Task<ApiResponse<CategoryEntity>> CreateAsync(string name)
        {
            var category = new CategoryEntity { CategoryName = name };
            await _category.AddAsync(category);

            return ApiResponse<CategoryEntity>.Success(category, Messages.Created);
        }
        public async Task<ApiResponse<string>> UpdateAsync(int id, string newName)
        {
            var category = await _category.GetByIdAsync(id);

            if (category == null)
                return ApiResponse<string>.Fail(Messages.NotFound, StatusCodes.NotFound);

            category.CategoryName = newName;
            await _category.UpdateAsync(category);

            return ApiResponse<string>.Success("Category updated", Messages.Updated);
        }
        public async Task<ApiResponse<string>> DeleteAsync(int id)
        {
            var category = await _category.GetByIdAsync(id);

            if (category == null)
                return ApiResponse<string>.Fail(Messages.NotFound, StatusCodes.NotFound);

            await _category.DeleteAsync(category);

            return ApiResponse<string>.Success("Category deleted", Messages.Deleted);
        }
    }
}
