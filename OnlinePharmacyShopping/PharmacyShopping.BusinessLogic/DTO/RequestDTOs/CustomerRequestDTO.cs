using PharmacyShopping.DataAccess.Models;

namespace PharmacyShopping.BusinessLogic.DTO.RequestDTOs
{
    public class CustomerRequestDTO
    {
        public string CustomerFirstName { get; set; }

        public string CustomerLastName { get; set; }

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public string PhoneNumber { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerPassword { get; set; }
    }
}