namespace PharmacyShopping.BusinessLogic.DTO.ResponseDTOs
{
    public class SalesResponseDTO
    {
        public int SaleId { get; set; }

        public int PharmacyId { get; set; }

        public int CustomerId { get; set; }

        public int MedicineId { get; set; }

        public int PurchaseId { get; set; }

        public DateTime SaleDate { get; set; }
    }
}
