using System.ComponentModel.DataAnnotations;

namespace TranDinhDuong_2280600533.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        public string ImageUrl { get; set; }

        // Link to Product
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
