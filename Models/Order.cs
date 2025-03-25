<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace TranDinhDuong_2280600533.Models
=======
﻿namespace TranDinhDuong_2280600533.Models
>>>>>>> dc6ad3aed38c4575237605cd2b0e7756b53b5f19
{
    public class Order
    {
        public int Id { get; set; }
<<<<<<< HEAD
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string ShippingAddress { get; set; }
        public string Notes { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
=======
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }

        // Nếu có các thuộc tính khác hoặc quan hệ, bổ sung ở đây.
>>>>>>> dc6ad3aed38c4575237605cd2b0e7756b53b5f19
    }
}
