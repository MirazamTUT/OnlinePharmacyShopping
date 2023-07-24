using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;

namespace PharmacyShopping.BusinessLogic.Service.IServices
{
    public interface IDataBaseService
    {
        Task<int> AddDataBaseAsync(DataBaseRequestDTO dataBaseRequestDTO);

        Task<DataBaseResponseDTO> GetDataBaseAsync();

        Task<int> UpdateDataBaseAsync(DataBaseRequestDTO dataBaseRequestDTO, int id);

        Task<int> DeleteDataBaseAsync(int id);
    }
}