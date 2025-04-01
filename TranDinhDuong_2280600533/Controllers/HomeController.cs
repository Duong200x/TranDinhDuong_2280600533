using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TranDinhDuong_2280600533.Models;
using TranDinhDuong_2280600533.Repositories;

namespace TranDinhDuong_2280600533.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;

        public HomeController(IProductRepository productRepository, ICartRepository cartRepository)
        {
            _productRepository = productRepository;
            _cartRepository = cartRepository;
        }

        // Trang chủ - Hiển thị danh sách sản phẩm
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }

        // Trang chính sách bảo mật
        public IActionResult Privacy()
        {
            return View();
        }

        // ✅ Hiển thị giỏ hàng
        [Authorize]
        public async Task<IActionResult> Cart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var cartItems = await _cartRepository.GetCartItemsAsync(userId);
            return View(cartItems);
        }

        // ✅ Thêm sản phẩm vào giỏ hàng
        [Authorize]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                await _cartRepository.AddToCartAsync(userId, productId);
                TempData["SuccessMessage"] = "Sản phẩm đã được thêm vào giỏ hàng!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi khi thêm vào giỏ hàng: " + ex.Message;
            }

            return RedirectToAction("Cart"); // Chuyển hướng đến trang giỏ hàng
        }

        // ✅ Cập nhật số lượng sản phẩm trong giỏ hàng
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateCart(int productId, int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                await _cartRepository.UpdateCartItemAsync(userId, productId, quantity);
                TempData["SuccessMessage"] = "Cập nhật giỏ hàng thành công!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi khi cập nhật giỏ hàng: " + ex.Message;
            }

            return RedirectToAction("Cart");
        }

        // ✅ Xóa sản phẩm khỏi giỏ hàng
        [Authorize]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                await _cartRepository.RemoveFromCartAsync(userId, productId);
                TempData["SuccessMessage"] = "Sản phẩm đã được xóa khỏi giỏ hàng!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Lỗi khi xóa sản phẩm: " + ex.Message;
            }

            return RedirectToAction("Cart");
        }

        // ✅ Xử lý lỗi chung
        [Route("Home/Error")]
        [HttpGet]  // Đảm bảo phương thức HTTP rõ ràng
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult GeneralError()
        {
            var errorViewModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View("Error", errorViewModel);
        }

        // ✅ Xử lý lỗi theo mã HTTP (404, 403, v.v.)
        [Route("Home/Error/{statusCode}")]
        [HttpGet]  // Đảm bảo phương thức HTTP rõ ràng
        public IActionResult HandleStatusCode(int statusCode)
        {
            var errorViewModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            if (statusCode == 403)
            {
                return View("AccessDenied", errorViewModel);
            }
            else if (statusCode == 404)
            {
                return View("NotFound", errorViewModel);
            }
            else
            {
                return View("Error", errorViewModel);
            }
        }
    }
}
