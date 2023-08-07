namespace PharmacyShopping.DataAccess.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }

        public int CustomerId { get; set; }

        public int SaleId { get; set; }

        public int MedicineId { get; set; }

        public int Amount { get; set; }

        public double TotalPrice { get; set; }


        public Customer Customer { get; set; }

        public Medicine Medicine { get; set; }

        public Sale Sale { get; set; }
    }
}