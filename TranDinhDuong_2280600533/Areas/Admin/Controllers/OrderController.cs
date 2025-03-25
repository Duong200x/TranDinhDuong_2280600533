<<<<<<< HEAD
﻿
=======
﻿using Microsoft.AspNetCore.Mvc;
using TranDinhDuong_2280600533.Models;
using TranDinhDuong_2280600533.Repositories;

namespace TranDinhDuong_2280600533.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // Hiển thị danh sách đơn hàng
        public IActionResult Index()
        {
            var orders = _orderRepository.GetAll();
            return View(orders);
        }

        // Hiển thị chi tiết đơn hàng
        public IActionResult Details(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // Trang xác nhận xóa đơn hàng
        public IActionResult Delete(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _orderRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
>>>>>>> dc6ad3aed38c4575237605cd2b0e7756b53b5f19
