namespace PharmacyShopping.DataAccess.Models
{
    public class Sales
    {
        public int SaleId { get; set; }

        public int PharmacyId { get; set; }

        public int CustomerId { get; set; }

        public int MedicineId { get; set; }

        public int PurchaseId { get; set; }

        public DateTime SaleDate { get; set; }

        public double TotalAmount { get; set; }


        public Pharmacy Pharmacy { get; set; }

        public List<Purchase> Purchases { get; set; }

        public List<Customer> Customers { get; set; }
    }
}
