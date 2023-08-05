namespace PharmacyShopping.BusinessLogic.DTO.RequestDTOs
{
    public class SaleRequestDTO
    {
        public int TotalAmount { get; set; }

        public int PharmacyId { get; set; }

        public int CustomerId { get; set; }
    }
}