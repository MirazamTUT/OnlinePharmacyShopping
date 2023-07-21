using Microsoft.EntityFrameworkCore;
using PharmacyShopping.DataAccess.DbConnection;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.IRepositories;

namespace PharmacyShopping.DataAccess.Repository.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        private readonly PharmacyDbContext _context;

        public SalesRepository(PharmacyDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddSalesAsync(Sales sales)
        {
            try
            {
                _context.Sales.Add(sales);
                await _context.SaveChangesAsync();
                return sales.SaleId;
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

        public async Task<int> DeleteSalesAsync(Sales sales)
        {
            try
            {
                _context.Sales.Remove(sales);
                await _context.SaveChangesAsync();
                return sales.SaleId;
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

        public async Task<List<Sales>> GetAllSalesAsync()
        {
            try
            {
                return await _context.Sales
                    .Include(u => u.Pharmacy)
                    .Include(u => u.Purchases)
                    .Include(u => u.Customer)
                    .AsSplitQuery()
                    .ToListAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation was failed when it was giving the information");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was giving Sales information");
            }
        }

        public async Task<Sales> GetSalesByCustomerIdAsync(int id)
        {
            try
            {
                return await _context.Sales
                   .Include(u => u.Pharmacy)
                   .Include(u => u.Purchases)
                   .Include(u => u.Customer)
                   .AsSplitQuery()
                   .FirstOrDefaultAsync(u => u.SaleId == id);
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation was failed when it was giving the information");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was giving SalesByCustomerId information");
            }
        }

        public async Task<int> UpdateSalesAsync(Sales sales)
        {
            try
            {
                _context.Sales.Update(sales);
                await _context.SaveChangesAsync();
                return sales.SaleId;
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
