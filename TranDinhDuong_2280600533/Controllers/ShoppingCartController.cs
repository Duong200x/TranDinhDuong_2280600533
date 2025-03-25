using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TranDinhDuong_2280600533.Extensions;
using TranDinhDuong_2280600533.Models;
using TranDinhDuong_2280600533.Repositories;

namespace TranDinhDuong_2280600533.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingCartController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IProductRepository productRepository,
            ICartRepository cartRepository)
        {
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _context = context;
            _userManager = userManager;
        }

        // Hiển thị trang thanh toán
        public IActionResult Checkout()
        {
            return View(new Order());
        }

        // Xử lý thanh toán
        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItems = await _cartRepository.GetCartItemsAsync(userId);

            if (cartItems == null || !cartItems.Any())
            {
                TempData["ErrorMessage"] = "Giỏ hàng của bạn đang trống!";
                return RedirectToAction("Index");
            }

            var user = await _userManager.GetUserAsync(User);
            order.UserId = user.Id;
            order.OrderDate = DateTime.UtcNow;
            order.TotalPrice = cartItems.Sum(i => i.Product.Price * i.Quantity);
            order.OrderDetails = cartItems.Select(i => new OrderDetail
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.Product.Price
            }).ToList();

            // Gán giá trị cho CustomerName
            order.CustomerName = user.UserName; // Hoặc lấy tên khác nếu cần

            // Gán giá trị cho OrderNumber
            order.OrderNumber = "ORD-" + Guid.NewGuid().ToString().Substring(0, 8); // Mã đơn hàng

            // Lưu đơn hàng vào cơ sở dữ liệu
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Xóa giỏ hàng sau khi thanh toán thành công
            await _cartRepository.ClearCartAsync(userId);

            return View("OrderCompleted", order.Id); // Hiển thị màn hình hoàn thành đơn hàng
        }

        // Thêm sản phẩm vào giỏ hàng
        public async Task<IActionResult> AddToCart(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            await _cartRepository.AddToCartAsync(userId, productId);
            return RedirectToAction("Index");
        }

        // Hiển thị giỏ hàng
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItems = await _cartRepository.GetCartItemsAsync(userId);

            // Tạo đối tượng ShoppingCart để chứa các CartItem
            var shoppingCart = new ShoppingCart
            {
                Items = (List<CartItem>)cartItems
            };

            return View(shoppingCart);  // Truyền ShoppingCart vào view
        }

        // Xóa sản phẩm khỏi giỏ hàng
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _cartRepository.RemoveFromCartAsync(userId, productId);
            return RedirectToAction("Index");
        }

        // Cập nhật số lượng sản phẩm trong giỏ
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int productId, int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (quantity < 1)
            {
                await _cartRepository.RemoveFromCartAsync(userId, productId);
            }
            else
            {
                await _cartRepository.UpdateCartItemQuantityAsync(userId, productId, quantity);
            }
            return RedirectToAction("Index");
        }

        // Xử lý việc hiển thị giỏ hàng trống và các thông báo cho người dùng
        public async Task<IActionResult> EmptyCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _cartRepository.ClearCartAsync(userId);
            TempData["Message"] = "Giỏ hàng của bạn đã được làm trống!";
            return RedirectToAction("Index");
        }
    }
}
