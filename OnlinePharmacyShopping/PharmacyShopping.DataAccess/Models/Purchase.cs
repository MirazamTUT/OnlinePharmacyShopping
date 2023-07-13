namespace PharmacyShopping.DataAccess.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }

        public int CustomerId { get; set; }

        public int MedicineId { get; set; }

        public double Amount { get; set; }

        public DateTime PurchaseDate { get; set; }


        public Sales Sales { get; set; }

        public Customer Customer { get; set; }

        public Medicine Medicine { get; set; }
    }
}
