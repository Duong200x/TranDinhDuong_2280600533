using Microsoft.AspNetCore.Mvc;
using TranDinhDuong_2280600533.Models;
using TranDinhDuong_2280600533.Repositories;

namespace TranDinhDuong_2280600533.Controllers
{
    // Đảm bảo rằng URL của API là "api/products"
    [Route("api/products")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        // Constructor
        public ProductApiController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
            // Trả về danh sách sản phẩm
            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound(); // Nếu sản phẩm không tồn tại, trả về lỗi 404
            }
            return Ok(product); // Trả về thông tin sản phẩm
        }

        // POST: api/products
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.AddAsync(product);
                // Sau khi thêm sản phẩm mới, trả về thông tin sản phẩm và URL của nó
                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
            }
            return BadRequest(ModelState); // Nếu dữ liệu không hợp lệ, trả về lỗi 400
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest(); // Nếu id không khớp, trả về lỗi 400
            }

            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound(); // Nếu sản phẩm không tồn tại, trả về lỗi 404
            }

            await _productRepository.UpdateAsync(product);
            return NoContent(); // Cập nhật thành công, không trả về nội dung
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound(); // Nếu sản phẩm không tồn tại, trả về lỗi 404
            }

            await _productRepository.DeleteAsync(id);
            return NoContent(); // Xóa thành công, không trả về nội dung
        }
    }
}
