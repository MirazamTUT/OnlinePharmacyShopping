using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PharmacyShopping.DataAccess.DbConnection;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.IRepositories;

namespace PharmacyShopping.DataAccess.Repository.Repositories
{
    public class DataBaseRepository : IDataBaseRepository
    {
        private readonly PharmacyDbContext _context;
        private readonly ILogger<DataBaseRepository> _logger;

        public DataBaseRepository(PharmacyDbContext context, ILogger<DataBaseRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> AddDataBaseAsync(DataBase dataBase)
        {
            try
            {
                _context.DataBases.Add(dataBase);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Datas was successfully added.");
                return dataBase.DataBaseId;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error adding Datas to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error adding Datas to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was adding changes.");
            }
        }

        public async Task<int> DeleteDataBaseAsync(DataBase dataBase)
        {
            try
            {
                _context.DataBases.Remove(dataBase);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Datas was successfully deleted.");
                return dataBase.DataBaseId;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error deleting Datas to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error deleting Datas to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was deleting changes.");
            }
        }

        public async Task<DataBase> GetDataBaseAsync()
        {
            try
            {
                _logger.LogInformation("Datas were found successfully.");
                return await _context.DataBases
                .Include(u => u.Pharmacy)
                .Include(u => u.Medicine)
                .AsSplitQuery()
                .FirstAsync();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving Datas in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving the information.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is an error retrieving Datas from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving database information.");
            }
        }

        public async Task<int> UpdateDataBaseAsync(DataBase dataBase)
        {
            try
            {
                _logger.LogInformation("Datas was successfully updated.");
                _context.DataBases.Update(dataBase);
                await _context.SaveChangesAsync();
                return dataBase.DataBaseId;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while updating Datas {dataBase.DataBaseId} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Connection between database is failed.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while updating Datas {dataBase.DataBaseId} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was updating changes.");
            }
        }
    }
}