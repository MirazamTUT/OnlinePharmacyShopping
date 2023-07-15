namespace PharmacyShopping.BusinessLogic.DTO.ResponseDTOs
{
    public class PurchaseResponseDTO
    {
        public int PurchaseId { get; set; }

        public int CustomerId { get; set; }

        public int MedicineId { get; set; }

        public DateTime PurchaseDate { get; set; }
    }
}
