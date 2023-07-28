namespace PharmacyShopping.DataAccess.Models
{
    public class DataBase
    {
        public int DataBaseId { get; set; }

        public string DataBaseName { get; set; }


        public List<Medicine> Medicine { get; set; }

        public Pharmacy Pharmacy { get; set; }
    }
}