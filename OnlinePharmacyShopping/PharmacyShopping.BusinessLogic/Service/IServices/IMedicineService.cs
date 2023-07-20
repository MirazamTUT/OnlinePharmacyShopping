using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;

namespace PharmacyShopping.BusinessLogic.Service.IServices
{
    public interface IMedicineService
    {
        Task<int> AddMedicineAsync(MedicineRequestDTO medicineRequestDTO);

        Task<MedicineResponseDTO> GetMedicineByIdAsync(int medicineId);

        Task<List<MedicineResponseDTO>> GetMedicinesAsync();

<<<<<<< HEAD
        Task<List<int>> GetAllReportMedicineByMedicineIdAsync(int reportId);

        Task<int> DeleteMedicineAsync(int medicineId);

        Task<int> UpdateMedicineAsync(MedicineRequestDTO medicineRequestDTO, int medicineId);
=======
        Task<int> DeleteMedicineAsync(int medicineId);

        Task<int> UpdateMedicineAsync(MedicineRequestDTO medicineRequestDTO, int medicineId);

>>>>>>> b700e155ba155bfce2e61fb6f534231594c258dc
    }
}
