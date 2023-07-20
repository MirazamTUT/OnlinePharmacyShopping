using Microsoft.EntityFrameworkCore;
using PharmacyShopping.DataAccess.DbConnection;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.IRepositories;

namespace PharmacyShopping.DataAccess.Repository.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly PharmacyDbContext _context;

        public ReportRepository(PharmacyDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddReportAsync(Report report)
        {
            try
            {
                _context.Reports.Add(report);
                await _context.SaveChangesAsync();
                return report.ReportId;
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

        public async Task<int> DeleteReportAsync(Report report)
        {
            try
            {
                _context.Reports.Remove(report);
                await _context.SaveChangesAsync();
                return report.ReportId;
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

        public async Task<List<Report>> GetAllReportsAsync()
        {
            try
            {
                return await _context.Reports
                   .Include(u => u.Customer)
                   .Include(u => u.ReportMedicines)
                   .AsSplitQuery()
                   .ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation was failed when it was giving the information");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was giving Reports information");
            }
        }

        public async Task<Report> GetReportByIdAsync(int id)
        {
            try
            {
                return await _context.Reports
                    .Include(u => u.Customer)
                    .Include(u => u.ReportMedicines)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(u => u.ReportId == id);
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation was failed when it was giving the information");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was giving ReportById information");
            }
        }

        public async Task<int> UpdateReportAsync(Report report)
        {
            try
            {
                _context.Reports.Update(report);
                await _context.SaveChangesAsync();
                return report.ReportId;
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
