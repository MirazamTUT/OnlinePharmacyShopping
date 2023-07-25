using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PharmacyShopping.DataAccess.DbConnection;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.IRepositories;

namespace PharmacyShopping.DataAccess.Repository.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        private readonly PharmacyDbContext _context;
        private readonly ILogger<SalesRepository> _logger;

        public SalesRepository(PharmacyDbContext context, ILogger<SalesRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> AddSalesAsync(Sales sales)
        {
            try
            {
                _context.Sales.Add(sales);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Sales was successfully added.");
                return sales.SaleId;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error adding Sales to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error saving Sales to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was adding changes.");
            }
        }

        public async Task<int> DeleteSalesAsync(Sales sales)
        {
            try
            {
                _context.Sales.Remove(sales);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Sales was successfully deleted.");
                return sales.SaleId;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error deleting Sales to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error deleting Sales to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was deleting changes.");
            }
        }

        public async Task<List<Sales>> GetAllSalesAsync()
        {
            try
            {
                _logger.LogInformation("All Sales were found successfully.");
                return await _context.Sales
                    .Include(u => u.Pharmacy)
                    .Include(u => u.Purchases)
                    .Include(u => u.Customer)
                    .AsSplitQuery()
                    .ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving all Sales in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving the information.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is an error retrieving all Sales from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving Sales information.");
            }
        }

        public async Task<Sales> GetSalesByCustomerIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("SalesById was found successfully.");
                return await _context.Sales
                   .Include(u => u.Pharmacy)
                   .Include(u => u.Purchases)
                   .Include(u => u.Customer)
                   .AsSplitQuery()
                   .FirstOrDefaultAsync(u => u.SaleId == id);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving SaleById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving the information.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while retrieving SaleById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving SalesByCustomerId information.");
            }
        }

        public async Task<int> UpdateSalesAsync(Sales sales)
        {
            try
            {
                _context.Sales.Update(sales);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Sales was successfully updated.");
                return sales.SaleId;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while updating Sales {sales.SaleId} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while updating Sales {sales.SaleId} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was updating changes.");
            }
        }
    }
}