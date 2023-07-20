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
            try
            {
                _context.Pharmacies.Add(pharmacy);
                await _context.SaveChangesAsync();
                return pharmacy.PharmacyId;
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

        public async Task<int> DeletePharmacyAsync(Pharmacy pharmacy)
        {
            try
            {
                _context.Pharmacies.Remove(pharmacy);
                await _context.SaveChangesAsync();
                return pharmacy.PharmacyId;
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

        public async Task<List<Pharmacy>> GetAllPharmacyAsync()
        {
            try
            {
                return await _context.Pharmacies
                    .Include(u => u.Sales)
                    .Include(u => u.Reports)
                    .AsSplitQuery()
                    .ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation was failed when it was giving the information");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was giving Pharmacys information");
            }
        }

        public async Task<Pharmacy> GetPharmacyByIdAsync(int id)
        {
            try
            {
                return await _context.Pharmacies
                 .Include(u => u.Sales)
                 .Include(u => u.Reports)
                 .AsSplitQuery()
                 .FirstOrDefaultAsync(u => u.PharmacyId == id);
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation was failed when it was giving the information");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was giving PharmacyById information");
            }
        }

        public async Task<int> UpdatePharmacyAsync(Pharmacy pharmacy)
        {
            try
            {
                _context.Pharmacies.Update(pharmacy);
                await _context.SaveChangesAsync();
                return pharmacy.PharmacyId;
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
