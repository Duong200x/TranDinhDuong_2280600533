using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranDinhDuong_2280600533.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; } // Mã người dùng

        [ForeignKey("Product")]
        public int ProductId { get; set; } // Mã sản phẩm

        public int Quantity { get; set; } // Số lượng

        public decimal Price { get; set; }  // Giá sản phẩm

        // Thêm thuộc tính này để tránh lỗi
        public string Name { get; set; } // Tên sản phẩm

        public virtual Product Product { get; set; } // Navigation property
    }
}
