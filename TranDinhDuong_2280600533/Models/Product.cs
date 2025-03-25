using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranDinhDuong_2280600533.Models
{
    public class Product 
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        // Relationship with Category
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        // Collection of Product Images
        public ICollection<ProductImage>? Images { get; set; }
        
    }
}
