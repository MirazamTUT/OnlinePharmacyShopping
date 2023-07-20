using Microsoft.EntityFrameworkCore;
using PharmacyShopping.DataAccess.DbConnection;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.IRepositories;

namespace PharmacyShopping.DataAccess.Repository.Repositories
{
    public class DataBaseRepository : IDataBaseRepository
    {
        private readonly PharmacyDbContext _context;

        public DataBaseRepository(PharmacyDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddDataBaseAsync(DataBase dataBase)
        {
            _context.DataBases.Add(dataBase);
            await _context.SaveChangesAsync();
            return dataBase.DataBaseId;
        }

        public async Task<int> DeleteDataBaseAsync(DataBase dataBase)
        {
            _context.DataBases.Remove(dataBase);
            await _context.SaveChangesAsync();
            return dataBase.DataBaseId;
        }

        public async Task<DataBase> GetDataBaseAsync() => await _context.DataBases
            .Include(u => u.Pharmacy)
            .Include(u => u.Medicine)
            .AsSplitQuery()
            .FirstAsync();

        public async Task<int> UpdateDataBaseAsync(DataBase dataBase)
        {
            _context.DataBases.Update(dataBase);
            await _context.SaveChangesAsync();
            return dataBase.DataBaseId;
        }
    }
}
