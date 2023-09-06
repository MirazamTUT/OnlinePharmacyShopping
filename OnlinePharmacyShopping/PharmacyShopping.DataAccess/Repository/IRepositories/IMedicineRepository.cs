using PharmacyShopping.DataAccess.Models;

namespace PharmacyShopping.DataAccess.Repository.IRepositories
{
    public interface IMedicineRepository
    {
        Task<int> AddMedicineAsync(Medicine medicine);

        Task<Medicine> GetMedicineByIdAsync(int id);

        Task<List<Medicine>> GetAllMedicinesAsync(string? searchWord);

        Task<int> DeleteMedicineAsync(Medicine medicine);

        Task<int> UpdateMedicineAsync(Medicine medicine);
    }
}