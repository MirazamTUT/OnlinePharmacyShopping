using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;

namespace PharmacyShopping.BusinessLogic.Service.IServices
{
    public interface ISalesService
    {
        Task<int> AddSalesAsync(SaleRequestDTO salesRequestDTO);

        Task<int> DeleteSalesAsync(int id);

        Task<int> UpdateSalesAsync(SaleRequestDTO salesRequestDTO, int id);

        Task<SaleResponseDTO> GetSalesByIdAsync(int id);

        Task<List<SaleResponseDTO>> GetAllSalesAsync();
    }
}
