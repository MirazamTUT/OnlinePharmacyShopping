namespace PharmacyShopping.BusinessLogic.DTO.RequestDTOs
{
    public class PurchaseRequestDTO
    {
        public double Amount { get; set; }

        public int CustomerId { get; set; }

        public int MedicineId { get; set; }

        public DateTime PurchaseDate { get; set; }
    }
}
