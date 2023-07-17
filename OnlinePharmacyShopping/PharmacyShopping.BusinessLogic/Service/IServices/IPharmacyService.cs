using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;
using PharmacyShopping.DataAccess.Models;

namespace PharmacyShopping.BusinessLogic.Service.IServices
{
    public interface IPharmacyService
    {
        Task<int> AddPharmacyAsync(PharmacyRequestDTO pharmacyRequestDto);

        Task<PharmacyResponseDTO> GetPharmacyByIdAsync(int id);

        Task<List<PharmacyResponseDTO>> GetAllPharmacyAsync();

        Task<int> UpdatePharmacyAsync(PharmacyRequestDTO pharmacyRequestDTO);

        Task<int> DeletePharmacyAsync(int id);
    }
}
