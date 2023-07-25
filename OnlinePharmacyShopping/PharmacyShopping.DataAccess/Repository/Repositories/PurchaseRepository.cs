using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PharmacyShopping.DataAccess.DbConnection;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.IRepositories;

namespace PharmacyShopping.DataAccess.Repository.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly PharmacyDbContext _pharmacyDbContext;
        private readonly ILogger<PurchaseRepository> _logger;

        public PurchaseRepository(PharmacyDbContext pharmacyDbContext, ILogger<PurchaseRepository> logger)
        {
            _pharmacyDbContext = pharmacyDbContext;
            _logger = logger;
        }

        public async Task<int> AddPurchaseAsync(Purchase purchase)
        {
            try
            {
                _pharmacyDbContext.Purchases.AddAsync(purchase);
                await _pharmacyDbContext.SaveChangesAsync();
                _logger.LogInformation("Purchase was successfully added.");
                return purchase.PurchaseId;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error adding Purchase to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error saving Purchase to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was adding changes.");
            }
        }

        public async Task<int> DeletePurchaseAsync(Purchase purchase)
        {
            try
            {
                _pharmacyDbContext.Purchases.Remove(purchase);
                await _pharmacyDbContext.SaveChangesAsync();
                _logger.LogInformation("Purchase was successfully deleted.");
                return purchase.PurchaseId;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error deleting Purchase to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error deleting Purchase to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was deleting changes.");
            }
        }

        public async Task<List<Purchase>> GetAllPurchasesAsync()
        {
            try
            {
                _logger.LogInformation("All Purchase were found successfully.");
                return await _pharmacyDbContext.Purchases
                    .Include(x => x.Sales)
                    .Include(x => x.Customer)
                    .Include(x => x.Medicine)
                    .AsSplitQuery()
                    .ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving all Purchases in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving the information.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is an error retrieving all Purchases from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving Purchases information.");
            }
        }

        public async Task<Purchase> GetPurchaseByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("PurchaseById was found successfully.");
                return await _pharmacyDbContext.Purchases
                    .Include(x => x.Sales)
                    .Include(x => x.Customer)
                    .Include(x => x.Medicine)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(x => x.PurchaseId == id);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving PurchaseById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving the information.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while retrieving PurchaseById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving PurchaseById information.");
            }
        }

        public async Task<int> UpdatePurchaseAsync(Purchase purchase)
        {
            try
            {
                _pharmacyDbContext.Purchases.Update(purchase);
                await _pharmacyDbContext.SaveChangesAsync();
                _logger.LogInformation("Purchase was successfully updated.");
                return purchase.PurchaseId;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while updating Purchase {purchase.PurchaseId} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while updating Purchase {purchase.PurchaseId} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was updating changes.");
            }
        }
    }
}