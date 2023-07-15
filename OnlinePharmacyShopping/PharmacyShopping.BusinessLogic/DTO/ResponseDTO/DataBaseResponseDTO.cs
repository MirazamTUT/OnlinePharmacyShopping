using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShopping.BusinessLogic.DTO.ResponseDTO
{
    public class DataBaseResponseDTO
    {
        public int DataBaseId { get; set; }

        public List<int> MedicineId { get; set; }
    }
}
