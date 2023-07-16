namespace PharmacyShopping.BusinessLogic.DTO.ResponseDTOs
{
    public class PurchaseResponseDTO
    {
        public int PurchaseId { get; set; }

        public string CustomerFullName { get; set; }

        public string MedicineName { get; set; }

        public DateTime PurchaseDate { get; set; }

        public double Amount { get; set; }
    }
}
