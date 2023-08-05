namespace PharmacyShopping.BusinessLogic.DTO.ResponseDTOs
{
    public class PurchaseResponseDTO
    {
        public int PurchaseId { get; set; }

        public string CustomerFullName { get; set; }

        public string MedicineName { get; set; }

        public int Amount { get; set; }

        public double TotalPrice { get; set; }
    }
}