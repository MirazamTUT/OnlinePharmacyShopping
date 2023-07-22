namespace PharmacyShopping.BusinessLogic.DTO.RequestDTOs
{
    public class SalesRequestDTO
    {
        public double TotalAmount { get; set; }

        public int PharmacyId { get; set; }

        public int CustomerId { get; set; }

        public int MedicineId { get; set; }

        public int PurchaseId { get; set; }
    }
}