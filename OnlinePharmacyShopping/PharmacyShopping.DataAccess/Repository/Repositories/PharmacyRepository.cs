using Microsoft.EntityFrameworkCore;
using PharmacyShopping.DataAccess.DbConnection;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.IRepositories;

namespace PharmacyShopping.DataAccess.Repository.Repositories
{
    public class PharmacyRepository : IPharmacyRepository
    {
        private readonly PharmacyDbContext _context;

        public PharmacyRepository(PharmacyDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddPharmacyAsync(Pharmacy pharmacy)
        {
            _context.Pharmacies.Add(pharmacy);
            await _context.SaveChangesAsync();
            return pharmacy.PharmacyId;
        }

        public async Task<int> DeletePharmacyAsync(Pharmacy pharmacy)
        {
            _context.Pharmacies.Remove(pharmacy);
            await _context.SaveChangesAsync();
            return pharmacy.PharmacyId;
        }

        public async Task<List<Pharmacy>> GetAllPharmacyAsync() => await _context.Pharmacies
            .Include(u => u.Sales)
            .Include(u => u.Reports)
            .AsSplitQuery()
            .ToListAsync();

        public async Task<Pharmacy> GetPharmacyByIdAsync(int id) => await _context.Pharmacies
            .Include(u => u.Sales)
            .Include(u => u.Reports)
            .AsSplitQuery()
            .FirstOrDefaultAsync(u => u.PharmacyId == id);

        public async Task<int> UpdatePharmacyAsync(Pharmacy pharmacy)
        {
            _context.Pharmacies.Update(pharmacy);
            await _context.SaveChangesAsync();
            return pharmacy.PharmacyId;
        }
    }
}
