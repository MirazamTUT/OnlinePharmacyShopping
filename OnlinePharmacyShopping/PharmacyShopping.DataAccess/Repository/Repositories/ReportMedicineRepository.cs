using Microsoft.EntityFrameworkCore;
using PharmacyShopping.DataAccess.DbConnection;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.IRepositories;

namespace PharmacyShopping.DataAccess.Repository.Repositories
{
    public class ReportMedicineRepository : IReportMedicineRepository
    {
        private readonly PharmacyDbContext _context;

        public ReportMedicineRepository(PharmacyDbContext context)
        {
            _context = context;
        }

        public async Task AddReportMedicineAsync(ReportMedicine reportMedicine)
        {
            _context.ReportMedicines.Add(reportMedicine);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReportMedicineAsync(ReportMedicine reportMedicine)
        {
            _context.ReportMedicines.Remove(reportMedicine);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ReportMedicine>> GetAllReportByMedicineIdsAsync(int id) => await _context.ReportMedicines
            .Include(u => u.Medicine)
            .Include(u => u.Report)
            .AsSplitQuery()
            .Where(u => u.ReportMedicineId == id)
            .ToListAsync();

        public async Task UpdateReportMedicineAsync(ReportMedicine reportMedicine)
        {
            _context.ReportMedicines.Update(reportMedicine);
            await _context.SaveChangesAsync();
        }
    }

}
