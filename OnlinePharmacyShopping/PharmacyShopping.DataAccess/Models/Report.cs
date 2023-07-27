namespace PharmacyShopping.DataAccess.Models
{
    public class Report
    {
        public int ReportId { get; set; }

        public int CustomerId { get; set; }

        public List<int> MedicineId { get; set; }

        public string ReportDescription { get; set; }

        public DateTime ReportDate { get; set; }


        public Customer Customer { get; set; }

        public List<ReportMedicine> ReportMedicines { get; set; }
    }
}