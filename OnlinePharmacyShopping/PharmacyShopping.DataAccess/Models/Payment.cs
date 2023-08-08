namespace PharmacyShopping.DataAccess.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public int SaleID { get; set; }

        public string CreditCardNumber { get; set; }

        public double TotalPrice { get; set; }


        public Sale Sale { get; set; }
    }
}