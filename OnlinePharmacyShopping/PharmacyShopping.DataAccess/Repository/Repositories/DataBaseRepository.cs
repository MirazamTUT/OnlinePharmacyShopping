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

        public async Task<List<DataBase>> GetAllDataBasesAsync() => await _context.DataBases
            .Include(u => u.Pharmacy)
            .Include(u => u.Medicine)
            .ToListAsync();

        public async Task<DataBase> GetDataBaseByIdAsync(int id) => await _context.DataBases
            .Include(u => u.Pharmacy)
            .Include(u => u.Medicine)
            .FirstOrDefaultAsync(u => u.DataBaseId == id);

        public async Task<int> UpdateDtaBaseAsync(DataBase dataBase)
        {
            _context.DataBases.Update(dataBase);
            await _context.SaveChangesAsync();
            return dataBase.DataBaseId;
        }
    }
}
