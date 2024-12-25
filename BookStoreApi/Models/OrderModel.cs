namespace BookStoreApi.Models
{
    public class OrderModel
    {
        public int? Id { get; set; }
        public int? BookId { get; set; } 
        public int Quantity { get; set; }
        public DateTime? Order_date { get; set; }
        public string? Book_name { get; set; }  
        public string Customer { get; set; }
        public int ? Order_total { get; set; }
        public int ? Order_status { get; set; } 
        public string ? Book_author { get; set; }
    }
}
