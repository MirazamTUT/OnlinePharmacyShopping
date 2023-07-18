using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;

namespace PharmacyShopping.BusinessLogic.Service.IServices
{
    public interface IPurchaseService
    {
        Task<int> AddPurchaseAsync(PurchaseRequestDTO purchaseRequestDTO);

        Task<int> UpdatePurchaseAsync(PurchaseRequestDTO purchaseRequestDTO, int purchaseId);

        Task<int> DeletePurchaseAsync(int purchaseId);

        Task<PurchaseResponseDTO> GetPurchaseByIdAsync(int purchaseId);

        Task<List<PurchaseResponseDTO>> GetAllPurchasesAsync();
    }
}
