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

        public int AddDataBase(DataBase dataBase)
        {
            _context.DataBases.Add(dataBase);
            _context.SaveChanges();
            return dataBase.DataBaseId;
        }

        public int DeleteDataBase(DataBase dataBase)
        {
            _context.DataBases.Remove(dataBase);
            _context.SaveChanges();
            return dataBase.DataBaseId;
        }

        public List<DataBase> GetAllDataBases() => _context.DataBases
            .Include(u => u.Pharmacy)
            .Include(u => u.Medicine)
            .ToList();

        public DataBase GetDataBaseById(int id) => _context.DataBases
            .Include(u => u.Pharmacy)
            .Include(u => u.Medicine)
            .FirstOrDefault(u => u.DataBaseId == id);

        public int UpdateDtaBase(DataBase dataBase)
        {
            _context.DataBases.Update(dataBase);
            _context.SaveChanges();
            return dataBase.DataBaseId;
        }
    }
}
