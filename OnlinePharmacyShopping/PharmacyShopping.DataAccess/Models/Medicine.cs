namespace PharmacyShopping.DataAccess.Models
{
    public class Medicine
    {
        public int MedicineId { get; set; }

        public int DataBaseId { get; set; }

        public string MedicineName { get; set; }

        public string MedicineCategory { get; set; }

        public string MedicineDescription { get; set; }

        public double MedicinePrice { get; set; }

        public int AmountOfMedecine { get; set; }


        public List<Report> Reports { get; set; }

        public List<Purchase> Purchase { get; set; }

        public DataBase DataBase { get; set; }
    }
}
