using PharmacyShopping.DataAccess.Models;

namespace PharmacyShopping.DataAccess.Repository.IRepositories
{
    public interface IDataBaseRepository
    {
        public int AddDataBase(DataBase dataBase);

        public int DeleteDataBase(DataBase dataBase);

        public List<DataBase> GetAllDataBases();

        public DataBase GetDataBaseById(int id);

        public int UpdateDtaBase(DataBase dataBase);
    }
}
