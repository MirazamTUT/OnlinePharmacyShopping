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
            try
            {
                _context.DataBases.Add(dataBase);
                await _context.SaveChangesAsync();
                return dataBase.DataBaseId;
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

        public async Task<int> DeleteDataBaseAsync(DataBase dataBase)
        {
            try
            {
                _context.DataBases.Remove(dataBase);
                await _context.SaveChangesAsync();
                return dataBase.DataBaseId;
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

        public async Task<DataBase> GetDataBaseAsync()
        {
            try
            {
                return await _context.DataBases
                .Include(u => u.Pharmacy)
                .Include(u => u.Medicine)
                .AsSplitQuery()
                .FirstAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation was failed when it was giving the information");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was giving database information");
            }
        }

        public async Task<int> UpdateDataBaseAsync(DataBase dataBase)
        {
            try
            {
                _context.DataBases.Update(dataBase);
                await _context.SaveChangesAsync();
                return dataBase.DataBaseId;
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
