using PharmacyShopping.DataAccess.DbConnection;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace PharmacyShopping.DataAccess.Repository.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly PharmacyDbContext _pharmacyDbContext;

        public PurchaseRepository(PharmacyDbContext pharmacyDbContext)
        {
            _pharmacyDbContext = pharmacyDbContext;
        }

        public async Task<int> AddPurchaseAsync(Purchase purchase)
        {
            try
            {
                _pharmacyDbContext.Purchases.AddAsync(purchase);
                await _pharmacyDbContext.SaveChangesAsync();
                return purchase.PurchaseId;
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

        public async Task<int> DeletePurchaseAsync(Purchase purchase)
        {
            try
            {
                _pharmacyDbContext.Purchases.Remove(purchase);
                await _pharmacyDbContext.SaveChangesAsync();
                return purchase.PurchaseId;
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

        public async Task<List<Purchase>> GetAllPurchasesAsync()
        {
            try
            {
                return await _pharmacyDbContext.Purchases
                    .Include(x => x.Sales)
                    .Include(x => x.Customer)
                    .Include(x => x.Medicine)
                    .AsSplitQuery()
                    .ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation was failed wnet it was giving the info");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was giving purchases information");
            }
        }

        public async Task<Purchase> GetPurchaseByIdAsync(int id)
        {
            try
            {
                return await _pharmacyDbContext.Purchases
                    .Include(x => x.Sales)
                    .Include(x => x.Customer)
                    .Include(x => x.Medicine)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(x => x.PurchaseId == id);
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation was failed wnet it was giving the info");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was giving purchases information");
            }
        }

        public async Task<int> UpdatePurchaseAsync(Purchase purchase)
        {
            try
            {
                _pharmacyDbContext.Purchases.Update(purchase);
                await _pharmacyDbContext.SaveChangesAsync();
                return purchase.PurchaseId;
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
