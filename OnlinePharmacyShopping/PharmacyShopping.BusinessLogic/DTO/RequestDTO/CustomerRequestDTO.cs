using PharmacyShopping.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShopping.BusinessLogic.DTO.RequestDTO
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
