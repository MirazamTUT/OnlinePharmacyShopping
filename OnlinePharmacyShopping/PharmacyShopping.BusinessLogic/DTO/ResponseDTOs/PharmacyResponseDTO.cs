namespace PharmacyShopping.BusinessLogic.DTO.ResponseDTOs
{
    public class PharmacyResponseDTO
    {
        public int PharmacyId { get; set; }

        public int DataBaseId { get; set; }

        public string PharmacyName { get; set; }

        public List<string> PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
