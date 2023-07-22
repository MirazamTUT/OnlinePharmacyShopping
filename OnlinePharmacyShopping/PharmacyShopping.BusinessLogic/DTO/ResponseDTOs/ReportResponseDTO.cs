namespace PharmacyShopping.BusinessLogic.DTO.ResponseDTOs
{
    public class ReportResponseDTO
    {
        public DateTime ReportDate { get; set; }

        public int ReportId { get; set; }

        public int PurchaseId { get; set; }

        public string CustomerFullName { get; set; }

        public string MedicineName { get; set; }

        public string ReportDescription { get; set; }
    }
}