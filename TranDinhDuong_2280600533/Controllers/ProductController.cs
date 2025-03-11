using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TranDinhDuong_2280600533.Models;
using TranDinhDuong_2280600533.Repositories;

namespace TranDinhDuong_2280600533.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _environment;

        public ProductController(IProductRepository productRepository,
                                 ICategoryRepository categoryRepository,
                                 IWebHostEnvironment environment)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _environment = environment;
        }

        // GET: Display list of products
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }

        // GET: Display form to add a product
        public async Task<IActionResult> Add()
        {
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        // POST: Add product with file upload support
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Product product, List<IFormFile> imageFiles)
        {
            if (ModelState.IsValid)
            {
                // Process image files if any
                if (imageFiles != null && imageFiles.Any())
                {
                    product.ProductImages = new List<ProductImage>();
                    foreach (var file in imageFiles)
                    {
                        if (file.Length > 0)
                        {
                            var fileName = Path.GetFileNameWithoutExtension(file.FileName) +
                                           "_" + Guid.NewGuid().ToString() +
                                           Path.GetExtension(file.FileName);
                            var uploads = Path.Combine(_environment.WebRootPath, "images");
                            if (!Directory.Exists(uploads))
                                Directory.CreateDirectory(uploads);
                            var filePath = Path.Combine(uploads, fileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }
                            product.ProductImages.Add(new ProductImage { ImageUrl = "/images/" + fileName });
                        }
                    }
                }
                await _productRepository.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(product);
        }

        // GET: Display form to update product
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Update product with new image upload support
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Product product, List<IFormFile> imageFiles)
        {
            if (ModelState.IsValid)
            {
                if (imageFiles != null && imageFiles.Any())
                {
                    if (product.ProductImages == null)
                        product.ProductImages = new List<ProductImage>();

                    foreach (var file in imageFiles)
                    {
                        if (file.Length > 0)
                        {
                            var fileName = Path.GetFileNameWithoutExtension(file.FileName) +
                                           "_" + Guid.NewGuid().ToString() +
                                           Path.GetExtension(file.FileName);
                            var uploads = Path.Combine(_environment.WebRootPath, "images");
                            if (!Directory.Exists(uploads))
                                Directory.CreateDirectory(uploads);
                            var filePath = Path.Combine(uploads, fileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }
                            product.ProductImages.Add(new ProductImage { ImageUrl = "/images/" + fileName });
                        }
                    }
                }
                await _productRepository.UpdateAsync(product);
                return RedirectToAction(nameof(Index));
            }
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Confirm deletion of a product
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Delete product
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Display product details including images
        public async Task<IActionResult> Display(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
