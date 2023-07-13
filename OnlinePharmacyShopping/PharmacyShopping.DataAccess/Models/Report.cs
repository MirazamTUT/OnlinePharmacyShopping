namespace PharmacyShopping.DataAccess.Models
{
    public class Report
    {
        public int ReportId { get; set; }

        public int PurchaseId { get; set; }

        public int CustomerId { get; set; }

        public int MedicineId { get; set; }

        public string ReportDescription { get; set; }

        public DateTime ReportDate { get; set; }


        public List<Customer> Customers { get; set; }

        public List<Medicine> Medicines { get; set; }
    }
}
