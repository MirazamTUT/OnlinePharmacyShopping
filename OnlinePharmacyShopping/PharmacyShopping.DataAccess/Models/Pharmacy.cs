namespace PharmacyShopping.DataAccess.Models
{
    public class Pharmacy
    {
        public int PharmacyId { get; set; }

        public int DataBaseId { get; set; }

        public string PharmacyName { get; set; }

        public List<string> PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }


        public DataBase DataBase { get; set; }

        public List<Sales> Sales { get; set; }

        public List<Report> Reports { get; set; }
    }
}