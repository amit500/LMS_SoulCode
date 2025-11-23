using CategoryEntity = LMS_SoulCode.Features.Course.Models.Category;
﻿using LMS_SoulCode.Features.Course.Models;

namespace LMS_SoulCode.Features.Course.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryEntity>> GetAllAsync();
        Task<CategoryEntity?> GetByIdAsync(int id);
        Task<CategoryEntity> AddAsync(CategoryEntity category);
        Task UpdateAsync(CategoryEntity category);
        Task DeleteAsync(CategoryEntity category);

    }
}
