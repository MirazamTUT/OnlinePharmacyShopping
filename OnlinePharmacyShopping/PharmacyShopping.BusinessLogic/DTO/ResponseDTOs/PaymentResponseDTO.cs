namespace PharmacyShopping.BusinessLogic.DTO.ResponseDTOs
{
    public class PaymentResponseDTO
    {
        public int PaymentId { get; set; }

        public int SaleID { get; set; }

        public string CreditCardNumber { get; set; }

        public double TotalPrice { get; set; }
    }
}