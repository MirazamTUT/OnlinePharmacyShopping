namespace PharmacyShopping.DataAccess.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string CustomerFirstName { get; set; }

        public string CustomerLastName { get; set; }

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public string PhoneNumber { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerPassword { get; set; }


        public List<Purchase> Purchases { get; set; }

        public List<Sale> Sales { get; set; }

        public List<Report> Reports { get; set; }
    }

    public enum Gender
    {
        Male,
        Famale
    }
}