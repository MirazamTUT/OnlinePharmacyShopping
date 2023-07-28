namespace PharmacyShopping.BusinessLogic.DTO.ResponseDTOs
{
    public class DataBaseResponseDTO
    {
        public int DataBaseId { get; set; }

        public List<int> MedicineId { get; set; }

        public string DataBaseName { get; set; }
    }
}