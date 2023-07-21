using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;
using PharmacyShopping.DataAccess.Models;

namespace PharmacyShopping.BusinessLogic.Service.IServices
{
    public interface IDataBaseService
    {
        Task<int> AddDataBaseAsync(DataBase dataBase);

        Task<DataBaseResponseDTO> GetDataBaseAsync();

        Task<int> UpdateDataBaseAsync(DataBase dataBase, int id);

        Task<int> DeleteDataBaseAsync(int id);
    }
}
