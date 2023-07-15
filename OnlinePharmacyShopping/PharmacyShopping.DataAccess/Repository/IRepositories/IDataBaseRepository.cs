using PharmacyShopping.DataAccess.Models;

namespace PharmacyShopping.DataAccess.Repository.IRepositories
{
    public interface IDataBaseRepository
    {
        Task<int> AddDataBaseAsync(DataBase dataBase);

        Task<int> DeleteDataBaseAsync(DataBase dataBase);

        Task<List<DataBase>> GetAllDataBasesAsync();

        Task<DataBase> GetDataBaseByIdAsync(int id);

        Task<int> UpdateDtaBaseAsync(DataBase dataBase);
    }
}
