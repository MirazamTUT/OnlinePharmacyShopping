namespace PharmacyShopping.BusinessLogic.DTO.ResponseDTOs
{
    public class CustomerResponseDTO
    {
        public int CustomerId { get; set; }

        public byte[] ContentOfImage { get; set; }

        public string ContentType { get; set; }

        public string CustomerFullName { get; set; }

        public string CustomerPhoneNumber { get; set; }

        public string CustomerEmail { get; set; }

        public Stream Image { get; set; }
    }
}