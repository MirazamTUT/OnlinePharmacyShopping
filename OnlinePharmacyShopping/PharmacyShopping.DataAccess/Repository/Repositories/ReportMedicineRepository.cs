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
            try
            {
                _context.ReportMedicines.Add(reportMedicine);
                await _context.SaveChangesAsync();
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

        public async Task DeleteReportMedicineAsync(ReportMedicine reportMedicine)
        {
            try
            {
                _context.ReportMedicines.Remove(reportMedicine);
                await _context.SaveChangesAsync();
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

        public async Task<List<ReportMedicine>> GetAllReportMedicineByReportIdAsync(int id)
        {
            try
            {
                return await _context.ReportMedicines
                    .Include(u => u.Medicine)
                    .Include(u => u.Report)
                    .AsSingleQuery()
                    .Where(u => u.ReportId == id)
                    .ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation was failed when it was giving the information");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was giving ReportMedicineByReportid information");
            }
        }

        public async Task<List<ReportMedicine>> GetAllReportMedicineAsync()
        {
            try
            {
                return await _context.ReportMedicines
                    .Include(u => u.Medicine)
                    .Include(u => u.Report)
                    .AsSingleQuery()
                    .ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation was failed when it was giving the information");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was giving ReportMedicine information");
            }
        }

        public async Task<List<ReportMedicine>> GetAllReportMedicineByMedicineIdAsync(int id)
        {
            try
            {
                return await _context.ReportMedicines
               .Include(u => u.Medicine)
               .Include(u => u.Report)
               .AsSplitQuery()
               .Where(u => u.MedicineId == id)
               .ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation was failed when it was giving the information");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was giving ReportMedicineByMedicineId information");
            }
        }

        public async Task UpdateReportMedicineAsync(ReportMedicine reportMedicine)
        {
            try
            {
                _context.ReportMedicines.Update(reportMedicine);
                await _context.SaveChangesAsync();
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