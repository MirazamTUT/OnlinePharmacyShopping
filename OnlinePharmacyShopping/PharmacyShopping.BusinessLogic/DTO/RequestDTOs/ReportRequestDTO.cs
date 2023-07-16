namespace PharmacyShopping.BusinessLogic.DTO.RequestDTOs
{
    public class ReportRequestDTO
    {
        public string ReportDescription { get; set; }

        public int PurchaseId { get; set; }

        public int CustomerId { get; set; }

        public int MedicineId { get; set; }
    }
}
