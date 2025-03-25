using Microsoft.EntityFrameworkCore;
using TranDinhDuong_2280600533.Models;

namespace TranDinhDuong_2280600533.Repositories
{
    public class EFProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EFProductRepository> _logger;

        public EFProductRepository(ApplicationDbContext context, ILogger<EFProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category) // Nếu cần Category
                .Include(p => p.Images)   // Thêm dòng này để load danh sách ảnh
                .ToListAsync();
            ;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            // Bao gồm Category và ProductImages nếu có
            return await _context.Products
                                 .Include(p => p.Category)
                                 .Include(p => p.Images)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding product");
                throw;
            }
        }

        public async Task UpdateAsync(Product product)
        {
            try
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product");
                throw;
            }
        }
    }
}
