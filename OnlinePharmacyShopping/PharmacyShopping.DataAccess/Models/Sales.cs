namespace PharmacyShopping.DataAccess.Models
{
    public class Sales
    {
        public int SaleId { get; set; }

        public int PharmacyId { get; set; }

        public int CustomerId { get; set; }

        public List<int> PurchaseId { get; set; }

        public DateTime SaleDate { get; set; }

        public double TotalAmount { get; set; }


        public Pharmacy Pharmacy { get; set; }

        public List<Purchase> Purchases { get; set; }

        public Customer Customer { get; set; }
    }
}
