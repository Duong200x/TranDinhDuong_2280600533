using System.ComponentModel.DataAnnotations;

namespace TranDinhDuong_2280600533.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        public string Url { get; set; } // Thêm thuộc tính này để lưu URL của hình ảnh

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }


}
