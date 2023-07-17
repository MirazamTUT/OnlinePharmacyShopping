using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;

namespace PharmacyShopping.BusinessLogic.Service.IServices
{
    public interface IDataBaseService
    {
        Task<int> AddDataBaseAsync(DataBase dataBase);

        Task<DataBaseResponseDTO> GetDataBaseAsync();
    }
}
