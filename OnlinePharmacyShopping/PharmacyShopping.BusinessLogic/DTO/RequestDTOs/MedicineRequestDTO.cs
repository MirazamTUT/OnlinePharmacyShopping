namespace PharmacyShopping.BusinessLogic.DTO.RequestDTOs
{
    public class MedicineRequestDTO
    {
        public int DataBaseId { get; set; }

        public string MedicineName { get; set; }

        public string MedicineCategory { get; set; }

        public string MedicineDescription { get; set; }

        public double MedicinePrice { get; set; }

        public double AmountOfMedecine { get; set; }
    }
}