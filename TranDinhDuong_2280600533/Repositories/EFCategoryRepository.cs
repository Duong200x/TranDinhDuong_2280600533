using Microsoft.EntityFrameworkCore;
using TranDinhDuong_2280600533.Models;

namespace TranDinhDuong_2280600533.Repositories
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EFCategoryRepository> _logger;

        public EFCategoryRepository(ApplicationDbContext context, ILogger<EFCategoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task AddAsync(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding category");
                throw;
            }
        }

        public async Task UpdateAsync(Category category)
        {
            try
            {
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating category");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category != null)
                {
                    _context.Categories.Remove(category);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting category");
                throw;
            }
        }
    }
}
