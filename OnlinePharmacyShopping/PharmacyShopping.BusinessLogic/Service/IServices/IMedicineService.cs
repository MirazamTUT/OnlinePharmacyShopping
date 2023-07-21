using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;

namespace PharmacyShopping.BusinessLogic.Service.IServices
{
    public interface IMedicineService
    {
        Task<int> AddMedicineAsync(MedicineRequestDTO medicineRequestDTO);

        Task<MedicineResponseDTO> GetMedicineByIdAsync(int medicineId);

        Task<List<MedicineResponseDTO>> GetMedicinesAsync();

        Task<List<int>> GetAllReportMedicineByMedicineIdAsync(int medicineId);

        Task<int> DeleteMedicineAsync(int medicineId);

        Task<int> UpdateMedicineAsync(MedicineRequestDTO medicineRequestDTO, int medicineId);
    }
}
