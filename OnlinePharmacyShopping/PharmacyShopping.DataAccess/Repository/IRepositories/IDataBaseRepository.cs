using PharmacyShopping.DataAccess.Models;

namespace PharmacyShopping.DataAccess.Repository.IRepositories
{
    public interface IDataBaseRepository
    {
        Task<int> AddDataBase(DataBase dataBase);

        Task<int> DeleteDataBase(DataBase dataBase);

        Task<List<DataBase>> GetAllDataBases();

        Task<DataBase> GetDataBaseById(int id);

        Task<int> UpdateDtaBase(DataBase dataBase);
    }
}
