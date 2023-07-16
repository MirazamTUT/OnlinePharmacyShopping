namespace PharmacyShopping.BusinessLogic.DTO.ResponseDTOs
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
