using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;

namespace PharmacyShopping.BusinessLogic.Service.IServices
{
    public interface ISalesService
    {
        Task<int> AddSalesAsync(SalesRequestDTO salesRequestDTO);

        Task<int> DeleteSalesAsync(int id);

        Task<int> UpdateSalesAsync(SalesRequestDTO salesRequestDTO, int id);

        Task<SalesResponseDTO> GetSalesByIdAsync(int id);

        Task<List<SalesResponseDTO>> GetAllSalesAsync();
    }
}
