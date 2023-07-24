using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PharmacyShopping.DataAccess.DbConnection;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.IRepositories;

namespace PharmacyShopping.DataAccess.Repository.Repositories
{
    public class ReportMedicineRepository : IReportMedicineRepository
    {
        private readonly PharmacyDbContext _context;
        private readonly ILogger<ReportMedicineRepository> _logger;

        public ReportMedicineRepository(PharmacyDbContext context, ILogger<ReportMedicineRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddReportMedicineAsync(ReportMedicine reportMedicine)
        {
            try
            {
                _context.ReportMedicines.Add(reportMedicine);
                await _context.SaveChangesAsync();
                _logger.LogInformation("ReportMedicine was successfully added.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error adding ReportMedicine to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error saving ReportMedicine to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was adding changes.");
            }
        }

        public async Task DeleteReportMedicineAsync(ReportMedicine reportMedicine)
        {
            try
            {
                _context.ReportMedicines.Remove(reportMedicine);
                await _context.SaveChangesAsync();
                _logger.LogInformation("ReportMedicine was successfully deleted.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error deleting ReportMedicine to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error deleting reportMedicine to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was deleting changes.");
            }
        }

        public async Task<List<ReportMedicine>> GetAllReportMedicineByReportIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("AllReportMedicineByReportIds was  found successfully.");
                return await _context.ReportMedicines
                    .Include(u => u.Medicine)
                    .Include(u => u.Report)
                    .AsSingleQuery()
                    .Where(u => u.ReportId == id)
                    .ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving all ReportMedicineByReportIds from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving the information.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while retrieving all ReportMedicineByReportIds from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving ReportMedicineByReportid information.");
            }
        }

        public async Task<List<ReportMedicine>> GetAllReportMedicineAsync()
        {
            try
            {
                _logger.LogInformation("All ReportMedicine were found successfully.");
                return await _context.ReportMedicines
                    .Include(u => u.Medicine)
                    .Include(u => u.Report)
                    .AsSingleQuery()
                    .ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving all ReportMedicines in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving the information.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is an error retrieving all ReportMedicines from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving ReportMedicine information.");
            }
        }

        public async Task<List<ReportMedicine>> GetAllReportMedicineByMedicineIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("ReportMedicineByMedicineIds was found successfully.");
                return await _context.ReportMedicines
               .Include(u => u.Medicine)
               .Include(u => u.Report)
               .AsSplitQuery()
               .Where(u => u.MedicineId == id)
               .ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving all ReportMedicineByMedicineIds from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving the information.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while retrieving all ReportMedicineByMedicineIds from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving ReportMedicineByMedicineId information.");
            }
        }

        public async Task UpdateReportMedicineAsync(ReportMedicine reportMedicine)
        {
            try
            {
                _context.ReportMedicines.Update(reportMedicine);
                await _context.SaveChangesAsync();
                _logger.LogInformation("ReportMedicine was successfully updated.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while updating ReportMedicine {reportMedicine.ReportMedicineId} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while updating ReportMedicine {reportMedicine.ReportMedicineId} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was updating changes.");
            }
        }
    }
}