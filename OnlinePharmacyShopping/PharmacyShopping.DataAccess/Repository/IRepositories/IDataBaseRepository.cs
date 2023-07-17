using PharmacyShopping.DataAccess.Models;

namespace PharmacyShopping.DataAccess.Repository.IRepositories
{
    public interface IDataBaseRepository
    {
        Task<int> AddDataBaseAsync(DataBase dataBase);

        Task<int> DeleteDataBaseAsync(DataBase dataBase);

        Task<List<DataBase>> GetDataBaseAsync();

        Task<int> UpdateDataBaseAsync(DataBase dataBase);
    }
}
