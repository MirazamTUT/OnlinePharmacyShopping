using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;

namespace PharmacyShopping.BusinessLogic.Service.IServices
{
    public interface IPharmacyService
    {
        Task<int> AddPharmacyAsync(PharmacyRequestDTO pharmacyRequestDto);

        Task<PharmacyResponseDTO> GetPharmacyByIdAsync(int id);

        Task<List<PharmacyResponseDTO>> GetAllPharmacyAsync();

        Task<int> UpdatePharmacyAsync(PharmacyRequestDTO pharmacyRequestDTO, int id);

        Task<int> DeletePharmacyAsync(int id);
    }
}