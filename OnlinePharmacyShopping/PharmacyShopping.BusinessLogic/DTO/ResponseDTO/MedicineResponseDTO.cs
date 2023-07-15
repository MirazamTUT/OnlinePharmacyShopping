using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShopping.BusinessLogic.DTO.ResponseDTO
{
    public class MedicineResponseDTO
    {
        public int MedicineId { get; set; }
        
        public int DataBaseId { get; set; }

        public string MedicineName { get; set; }

        public string MedicineCategory { get; set; }

        public string MedicineDescription { get; set; }

        public double MedicinePrice { get; set; }

        public double AmountOfMedicine { get; set; }
    }
}
