﻿using System.ComponentModel.DataAnnotations;

namespace TranDinhDuong_2280600533.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        public string Name { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
