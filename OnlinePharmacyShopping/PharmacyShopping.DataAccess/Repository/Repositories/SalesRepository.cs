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
            _context.Sales.Add(sales);
            await _context.SaveChangesAsync();
            return sales.SaleId;
        }

        public async Task<int> DeleteSalesAsync(Sales sales)
        {
            _context.Sales.Remove(sales);
            await _context.SaveChangesAsync();
            return sales.SaleId;
        }

        public async Task<List<Sales>> GetAllSalesAsync() => await _context.Sales
            .Include(u => u.Pharmacy)
            .Include(u => u.Purchases)
            .Include(u => u.Customer)
            .AsSplitQuery()
            .ToListAsync();

        public async Task<Sales> GetSalesByCustomerIdAsync(int id) => await _context.Sales
            .Include(u => u.Pharmacy)
            .Include(u => u.Purchases)
            .Include(u => u.Customer)
            .AsSplitQuery()
            .FirstOrDefaultAsync(u => u.SaleId == id);

        public async Task<int> UpdateSalesAsync(Sales sales)
        {
            _context.Sales.Update(sales);
            await _context.SaveChangesAsync();
            return sales.SaleId;
        }
    }
}
