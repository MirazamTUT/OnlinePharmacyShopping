using PharmacyShopping.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShopping.BusinessLogic.DTO.ResponseDTO
{
    public class CustomerResponseDTO
    {
        public int CustomerId { get; set; }

        public string CustomerFullName { get; set; }

        public string CustomerPhoneNumber { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerPassword { get; set; }
    }
}
