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
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return report.ReportId;
        }

        public async Task<int> DeleteReportAsync(Report report)
        {
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return report.ReportId;
        }

        public async Task<List<Report>> GetAllReportsAsync() => await _context.Reports
            .Include(u => u.Customer)
            .Include(u => u.ReportMedicines)
            .ToListAsync();

        public async Task<Report> GetReportByIdAsync(int id) => await _context.Reports
            .Include(u => u.Customer)
            .Include(u => u.ReportMedicines)
            .FirstOrDefaultAsync(u => u.ReportId == id);

        public async Task<int> UpdateReportAsync(Report report)
        {
            _context.Reports.Update(report);
            await _context.SaveChangesAsync();
            return report.ReportId;
        }
    }
}
