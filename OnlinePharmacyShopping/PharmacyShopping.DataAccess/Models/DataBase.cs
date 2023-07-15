namespace PharmacyShopping.DataAccess.Models
{
    public class DataBase
    {
        public int DataBaseId { get; set; }

        public List<int> MedicineId { get; set; }


        public List<Medicine> Medicine { get; set; }

        public Pharmacy Pharmacy { get; set; }
    }
}
