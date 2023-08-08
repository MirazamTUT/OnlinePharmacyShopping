using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PharmacyShopping.DataAccess.DbConnection;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.IRepositories;

namespace PharmacyShopping.DataAccess.Repository.Repositories
{
    public class PharmacyRepository : IPharmacyRepository
    {
        private readonly PharmacyDbContext _context;
        private readonly ILogger<PharmacyRepository> _logger;

        public PharmacyRepository(PharmacyDbContext context, ILogger<PharmacyRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> AddPharmacyAsync(Pharmacy pharmacy)
        {
            try
            {
                _context.Pharmacies.Add(pharmacy);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Pharmacy was successfully added.");
                return pharmacy.PharmacyId;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error adding Pharmacy to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error saving Pharmacy to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was adding changes.");
            }
        }

        public async Task<int> DeletePharmacyAsync(Pharmacy pharmacy)
        {
            try
            {
                _context.Pharmacies.Remove(pharmacy);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Pharmacy was successfully deleted.");
                return pharmacy.PharmacyId;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error deleting Pharmacy to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error deleting Pharmacy to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was deleting changes.");
            }
        }

        public async Task<List<Pharmacy>> GetAllPharmaciesAsync()
        {
            try
            {
                _logger.LogInformation("All Pharmacies were found successfully.");
                return await _context.Pharmacies
                    .Include(u => u.Sales)
                    .Include(u => u.Reports)
                    .AsSplitQuery()
                    .ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving all Pharmacies in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving the information.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is an error retrieving all Pharmacies from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving Pharmacies information.");
            }
        }

        public async Task<Pharmacy> GetPharmacyByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("PharmacyById was found successfully.");
                return await _context.Pharmacies
                 .Include(u => u.Sales)
                 .Include(u => u.Reports)
                 .AsSplitQuery()
                 .FirstOrDefaultAsync(u => u.PharmacyId == id);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving PharmacyById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving the information.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while retrieving PharmacyById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving PharmacyById information.");
            }
        }

        public async Task<int> UpdatePharmacyAsync(Pharmacy pharmacy)
        {
            try
            {
                _context.Pharmacies.Update(pharmacy);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Pharmacy was successfully updated.");
                return pharmacy.PharmacyId;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while updating Pharmacy {pharmacy.PharmacyId} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while updating Pharmacy {pharmacy.PharmacyId} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was updating changes.");
            }
        }
    }
}
