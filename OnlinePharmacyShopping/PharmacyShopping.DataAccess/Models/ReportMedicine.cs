namespace PharmacyShopping.DataAccess.Models
{
    public class ReportMedicine
    {
        public int ReportMedicineId { get; set; }

        public int ReportId { get; set; }

        public string MedicineId { get; set; }


        public Medicine Medicine { get; set; }
        public Report Report { get; set; }
    }
}
