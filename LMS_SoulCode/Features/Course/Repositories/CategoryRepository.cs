using LMS_SoulCode.Data;
using LMS_SoulCode.Features.Course.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS_SoulCode.Features.Course.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LmsDbContext _context;
        public CategoryRepository(LmsDbContext context) => _context = context;

        public async Task<IEnumerable<Category>> GetAllAsync()
            => await _context.Categories.ToListAsync();

        public async Task<Category?> GetByIdAsync(int id)
            => await _context.Categories.FindAsync(id);

        public async Task<Category> AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
