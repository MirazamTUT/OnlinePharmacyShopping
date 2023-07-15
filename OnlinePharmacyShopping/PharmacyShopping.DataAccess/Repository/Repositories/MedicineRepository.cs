using Microsoft.EntityFrameworkCore;
using PharmacyShopping.DataAccess.DbConnection;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.IRepositories;

namespace PharmacyShopping.DataAccess.Repository.Repostories
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly PharmacyDbContext _context;

        public MedicineRepository(PharmacyDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddMedicineAsync(Medicine medicine)
        {
            _context.Medicines.Add(medicine);
            await _context.SaveChangesAsync();
            return medicine.MedicineId;
        }

        public async Task<int> DeleteMedicineAsync(Medicine medicine)
        {
            _context.Medicines.Remove(medicine);
            await _context.SaveChangesAsync();
            return medicine.MedicineId;
        }

        public async Task<List<Medicine>> GetAllMedicinesAsync() => await _context.Medicines
            .Include(u => u.Reports)
            .Include(u => u.Purchase)
            .Include(u => u.DataBase)
            .ToListAsync();

        public async Task<Medicine> GetMedicineByIdAsync(int id) => await _context.Medicines
            .Include(u => u.Reports)
            .Include(u => u.Purchase)
            .Include(u => u.DataBase)
            .FirstOrDefaultAsync(u => u.MedicineId == id);

        public async Task<int> UpdateMedicineAsync(Medicine medicine)
        {
            _context.Medicines.Update(medicine);
            await _context.SaveChangesAsync();
            return medicine.MedicineId;
        }
    }
}
