using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PharmacyShopping.DataAccess.DbConnection;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.IRepositories;

namespace PharmacyShopping.DataAccess.Repository.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly PharmacyDbContext _context;
        private readonly ILogger<ReportRepository> _logger;

        public ReportRepository(PharmacyDbContext context, ILogger<ReportRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> AddReportAsync(Report report)
        {
            try
            {
                _context.Reports.Add(report);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Report was successfully added.");
                return report.ReportId;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error adding Report to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error saving Report to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was adding changes.");
            }
        }

        public async Task<int> DeleteReportAsync(Report report)
        {
            try
            {
                _context.Reports.Remove(report);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Report was successfully deleted.");
                return report.ReportId;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error deleting Report to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error deleting Report to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was deleting changes.");
            }
        }

        public async Task<List<Report>> GetAllReportsAsync()
        {
            try
            {
                _logger.LogInformation("All Reports were found successfully.");
                return await _context.Reports
                   .Include(u => u.Customer)
                   .Include(u => u.ReportMedicines)
                   .AsSplitQuery()
                   .ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving all Reports in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving the information.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is an error retrieving all Reports from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving Reports information.");
            }
        }

        public async Task<Report> GetReportByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("ReportById was found successfully.");
                return await _context.Reports
                    .Include(u => u.Customer)
                    .Include(u => u.ReportMedicines)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(u => u.ReportId == id);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving ReportById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving the information.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while retrieving ReportById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving ReportById information.");
            }
        }

        public async Task<int> UpdateReportAsync(Report report)
        {
            try
            {
                _context.Reports.Update(report);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Report was successfully updated.");
                return report.ReportId;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while updating Report {report.ReportId} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while updating Report {report.ReportId} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was updating changes.");
            }
        }
    }
}
