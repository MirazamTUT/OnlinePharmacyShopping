using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PharmacyShopping.DataAccess.DbConnection;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.IRepositories;

namespace PharmacyShopping.DataAccess.Repository.Repositories
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly PharmacyDbContext _context;
        private readonly ILogger<MedicineRepository> _logger;

        public MedicineRepository(PharmacyDbContext context, ILogger<MedicineRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> AddMedicineAsync(Medicine medicine)
        {
            try
            {
                _context.Medicines.Add(medicine);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Medicine was successfully added.");
                return medicine.MedicineId;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error adding Medicine to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error saving Medicine to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was adding changes.");
            }
        }

        public async Task<int> DeleteMedicineAsync(Medicine medicine)
        {
            try
            {
                _context.Medicines.Remove(medicine);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Medicine was successfully deleted.");
                return medicine.MedicineId;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error deleting Medicine to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error deleting Medicine to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was deleting changes.");
            }
        }

        public async Task<List<Medicine>> GetAllMedicinesAsync(string? searchWord)
        {
            try
            {
                var allMedicines = await _context.Medicines
                   .Include(u => u.ReportMedicines)
                   .Include(u => u.Purchase)
                   .Include(u => u.DataBase)
                   .AsSplitQuery()
                   .ToListAsync();
                if (!string.IsNullOrEmpty(searchWord))
                {
                    allMedicines = allMedicines.Where(n => n.MedicineName.Contains(searchWord)).ToList();
                }
                else
                {
                    allMedicines = allMedicines.OrderBy(n => n.MedicineId).ToList();
                }
                _logger.LogInformation("All Medicines were found successfully.");
                return allMedicines;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving all Medicines in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving the information.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is an error retrieving all Medicines from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving Medicines information.");
            }
        }

        public async Task<Medicine> GetMedicineByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("MedicineById was found successfully.");
                return await _context.Medicines
                    .Include(u => u.ReportMedicines)
                    .Include(u => u.Purchase)
                    .Include(u => u.DataBase)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(u => u.MedicineId == id);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving MedicineById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving the information.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while retrieving MedicineById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving MedicineById information.");
            }
        }

        public async Task<int> UpdateMedicineAsync(Medicine medicine)
        {
            try
            {
                _context.Medicines.Update(medicine);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Medicine was successfully updated.");
                return medicine.MedicineId;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while updating Medicine {medicine.MedicineId} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while updating Medicine {medicine.MedicineId} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was updating changes.");
            }
        }
    }
}