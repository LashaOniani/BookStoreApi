namespace BookStoreApi.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Book_name { get; set; }   
        public string author { get; set; }  
        public double Price { get; set; }
        public int Quantity { get; set; }   
    }
}
