using TranDinhDuong_2280600533.Models;

namespace TranDinhDuong_2280600533.Repositories
{
    public interface ICartRepository
    {
        Task AddToCartAsync(string userId, int productId);
        Task<IEnumerable<CartItem>> GetCartItemsAsync(string userId);
        Task RemoveFromCartAsync(string userId, int productId);
        Task ClearCartAsync(string userId);
        Task UpdateCartItemAsync(string userId, int productId, int quantity);
        Task UpdateCartItemQuantityAsync(string userId, int productId, int quantity);
    }
}
