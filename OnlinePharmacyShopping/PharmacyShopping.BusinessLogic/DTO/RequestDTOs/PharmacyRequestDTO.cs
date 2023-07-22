namespace PharmacyShopping.BusinessLogic.DTO.RequestDTOs
{
    public class PharmacyRequestDTO
    {
        public string PharmacyName { get; set; }

        public int DataBaseId { get; set; }

        public List<string> PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}