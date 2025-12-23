namespace projectApiAngular.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        //customerId
        public int CustomerId { get; set; }
        public required User Castomer { get; set; }
           
        public int GiftId { get; set; }

        public required Gift Gift { get; set; }
        
        public DateTime PurchaseDate { get; set; }



    }
}
