<<<<<<< HEAD
﻿
=======
﻿using Microsoft.AspNetCore.Mvc;
using TranDinhDuong_2280600533.Models;
using TranDinhDuong_2280600533.Repositories;

namespace TranDinhDuong_2280600533.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        // Constructor tiêm các repository (không bị mất phần quan trọng)
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        // Action Index: Hiển thị danh sách sản phẩm
        public IActionResult Index()
        {
            var products = _productRepository.GetAll();
            return View(products);
        }

        // Action Add (GET): Hiển thị form thêm sản phẩm
        public IActionResult Add()
        {
            ViewBag.Categories = _categoryRepository.GetAll(); // truyền danh sách Category cho view nếu cần
            return View();
        }

        // Action Add (POST): Xử lý thêm sản phẩm
        [HttpPost]
        public IActionResult Add(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Add(product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _categoryRepository.GetAll();
            return View(product);
        }

        // Action Edit (GET): Hiển thị form sửa sản phẩm
        public IActionResult Edit(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = _categoryRepository.GetAll();
            return View(product);
        }

        // Action Edit (POST): Xử lý cập nhật sản phẩm
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Update(product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _categoryRepository.GetAll();
            return View(product);
        }

        // Action Delete (GET): Hiển thị trang xác nhận xóa sản phẩm
        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // Action Delete (POST): Xác nhận xóa sản phẩm
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _productRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
>>>>>>> dc6ad3aed38c4575237605cd2b0e7756b53b5f19
