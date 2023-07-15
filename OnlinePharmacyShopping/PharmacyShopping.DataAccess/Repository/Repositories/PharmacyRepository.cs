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

        public Task<int> AddPharmacy(Pharmacy pharmacy)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddPharmacyAsync(Pharmacy pharmacy)
        {
            _context.Pharmacies.Add(pharmacy);
            _context.SaveChanges();
            return pharmacy.PharmacyId;
        }

        public Task<int> DeletePharmacy(Pharmacy pharmacy)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeletePharmacyAsync(Pharmacy pharmacy)
        {
            _context.Pharmacies.Remove(pharmacy);
            _context.SaveChangesAsync();
            return pharmacy.PharmacyId;
        }

        public Task<List<Pharmacy>> GetAllPharmacy()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Pharmacy>> GetAllPharmacyAsync()
        {
            return await _context.Pharmacies
                .Include(u => u.Sales)
                .Include(u => u.Reports)
                .ToListAsync();
        }

        public async Task<Pharmacy> GetPharmacyById(int pharmacyId)
        {
            return await _context.Pharmacies
                .Include(u => u.Sales)
                .Include(u => u.Reports)
                .FirstOrDefaultAsync(u => u.PharmacyId == pharmacyId);
        }

        public async Task<int> UpdatePharmacy(Pharmacy pharmacy)
        {
            _context.Pharmacies.Update(pharmacy);
            _context.SaveChanges();
            return pharmacy.PharmacyId;
        }
    }
}