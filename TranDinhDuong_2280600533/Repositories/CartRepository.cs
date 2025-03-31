using Microsoft.EntityFrameworkCore;
using TranDinhDuong_2280600533.Models;

namespace TranDinhDuong_2280600533.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Thêm sản phẩm vào giỏ hàng
        public async Task AddToCartAsync(string userId, int productId)
        {
            // Kiểm tra sản phẩm có tồn tại không
            var product = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                throw new ArgumentException("Sản phẩm không tồn tại.");
            }

            // Kiểm tra nếu sản phẩm đã có trong giỏ hàng
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (cartItem != null)
            {
                // Nếu đã có sản phẩm, tăng số lượng
                cartItem.Quantity += 1;
            }
            else
            {
                // Nếu chưa có sản phẩm trong giỏ hàng, thêm mới
                cartItem = new CartItem
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = 1,
                    Price = product.Price,  // Lưu giá sản phẩm
                    Name = product.Name     // Lấy tên sản phẩm
                };
                await _context.CartItems.AddAsync(cartItem);
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();
        }

        // Cập nhật số lượng sản phẩm trong giỏ hàng
        public async Task UpdateCartItemAsync(string userId, int productId, int quantity)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (cartItem != null)
            {
                if (quantity > 0)
                {
                    // Cập nhật số lượng nếu lớn hơn 0
                    cartItem.Quantity = quantity;
                }
                else
                {
                    // Nếu số lượng nhỏ hơn hoặc bằng 0, xóa sản phẩm khỏi giỏ
                    _context.CartItems.Remove(cartItem);
                }

                await _context.SaveChangesAsync();
            }
        }

        // Lấy tất cả các sản phẩm trong giỏ hàng của người dùng
        public async Task<IEnumerable<CartItem>> GetCartItemsAsync(string userId)
        {
            return await _context.CartItems
                .Include(c => c.Product)  // Lấy thông tin sản phẩm liên quan
                .Where(c => c.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        // Xóa sản phẩm khỏi giỏ hàng
        public async Task RemoveFromCartAsync(string userId, int productId)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        // Xóa tất cả sản phẩm trong giỏ hàng
        public async Task ClearCartAsync(string userId)
        {
            var cartItems = _context.CartItems.Where(c => c.UserId == userId);
            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }

        // Cập nhật số lượng sản phẩm trong giỏ hàng
        public async Task UpdateCartItemQuantityAsync(string userId, int productId, int quantity)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                await _context.SaveChangesAsync();
            }
        }
    }
}
