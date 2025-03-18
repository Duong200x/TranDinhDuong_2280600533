namespace TranDinhDuong_2280600533.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }

        // Nếu có các thuộc tính khác hoặc quan hệ, bổ sung ở đây.
    }
}
