namespace PharmacyShopping.BusinessLogic.DTO.ResponseDTOs
{
    public class ReportResponseDTO
    {
        public DateTime ReportDate { get; set; }

        public int ReportId { get; set; }

        public int PurchaseId { get; set; }

        public int CustomerId { get; set; }

        public int MedicineId { get; set; }

    }
}
