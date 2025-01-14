namespace MyShop.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int BicycleId { get; set; }
        public Bicycle Bicycle { get; set; }

        public int? Quantity { get; set; }

        public DateTime PurchaseDate { get; set; }

        public decimal? Total { get; set; }
    }
}
