namespace PharmacyShopping.BusinessLogic.DTO.RequestDTOs
{
    public class ReportRequestDTO
    {
        public string ReportDescription { get; set; }

        public int PurchaseId { get; set; }

        public int CustomerId { get; set; }

        public List<int> MedicineId { get; set; }
    }
}
