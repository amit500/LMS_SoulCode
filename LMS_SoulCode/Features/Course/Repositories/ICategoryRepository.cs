using CategoryEntity = LMS_SoulCode.Features.Course.Entities.Category;
﻿using LMS_SoulCode.Features.Course.Entities;

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
