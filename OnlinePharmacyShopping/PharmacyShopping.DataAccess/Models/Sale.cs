namespace PharmacyShopping.DataAccess.Models
{
    public class Sale
    {
        public int SaleId { get; set; }

        public int PharmacyId { get; set; }

        public int CustomerId { get; set; }

        public DateTime SaleDate { get; set; }

        public int TotalAmount { get; set; }

        public double TotalPrice { get; set; }


        public Pharmacy Pharmacy { get; set; }

        public List<Purchase> Purchases { get; set; }

        public Customer Customer { get; set; }

        public Payment Payment { get; set; }
    }
}