using Microsoft.EntityFrameworkCore;
using PharmacyShopping.DataAccess.DbConnection;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.IRepositories;

namespace PharmacyShopping.DataAccess.Repository.Repositories
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
            try
            {
                _context.Medicines.Add(medicine);
                await _context.SaveChangesAsync();
                return medicine.MedicineId;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Connection between database is failed");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was adding changes");
            }
        }

        public async Task<int> DeleteMedicineAsync(Medicine medicine)
        {
            try
            {
                _context.Medicines.Remove(medicine);
                await _context.SaveChangesAsync();
                return medicine.MedicineId;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Connection between database is failed");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was deleting changes");
            }
        }

        public async Task<List<Medicine>> GetAllMedicinesAsync()
        {
            try
            {
                return await _context.Medicines
                   .Include(u => u.ReportMedicines)
                   .Include(u => u.Purchase)
                   .Include(u => u.DataBase)
                   .AsSplitQuery()
                   .ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation was failed when it was giving the information");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was giving Medicines information");
            }
        }

        public async Task<Medicine> GetMedicineByIdAsync(int id)
        {
            try
            {
                return await _context.Medicines
                    .Include(u => u.ReportMedicines)
                    .Include(u => u.Purchase)
                    .Include(u => u.DataBase)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(u => u.MedicineId == id);
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation was failed when it was giving the information");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was giving MedicineById information");
            }
        }

        public async Task<int> UpdateMedicineAsync(Medicine medicine)
        {
            try
            {
                _context.Medicines.Update(medicine);
                await _context.SaveChangesAsync();
                return medicine.MedicineId;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Connection between database is failed");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was updating changes");
            }
        }
    }
}