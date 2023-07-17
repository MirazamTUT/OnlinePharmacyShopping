using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;
using PharmacyShopping.BusinessLogic.Service.IServices;
using PharmacyShopping.DataAccess.Repository.IRepositories;

namespace PharmacyShopping.BusinessLogic.Service.Services
{
    public class PharmacyService : IPharmacyService
    {
        private readonly IPharmacyRepository _repository;

        public PharmacyService(IPharmacyRepository repository)
        {
            _repository = repository;
        }

        public Task<int> AddPharmacyAsync(PharmacyRequestDTO pharmacyRequestDto)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeletePharmacyAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PharmacyResponseDTO>> GetAllPharmacyAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PharmacyResponseDTO> GetPharmacyByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdatePharmacyAsync(PharmacyRequestDTO pharmacyRequestDTO)
        {
            throw new NotImplementedException();
        }
    }
}
