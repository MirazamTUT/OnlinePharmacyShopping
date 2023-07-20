using AutoMapper;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;
using PharmacyShopping.BusinessLogic.Service.IServices;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.IRepositories;

namespace PharmacyShopping.BusinessLogic.Service.Services
{
    public class PharmacyService : IPharmacyService
    {
        private readonly IMapper _mapper;
        private readonly IPharmacyRepository _repository;

        public PharmacyService(IPharmacyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> AddPharmacyAsync(PharmacyRequestDTO pharmacyRequestDto)
        {
            return await _repository.AddPharmacyAsync(_mapper.Map<Pharmacy>(pharmacyRequestDto));
        }

        public async Task<int> DeletePharmacyAsync(int id)
        {
            var resultCheckingPharmasy = await _repository.GetPharmacyByIdAsync(id);
            if (resultCheckingPharmasy is not null)
            {
                return await _repository.DeletePharmacyAsync(resultCheckingPharmasy);
            }
            else
            {
                throw new Exception("Object cannot be deleted");
            }
        }

        public async Task<List<PharmacyResponseDTO>> GetAllPharmacyAsync()
        {
            return _mapper.Map<List<PharmacyResponseDTO>>(await  _repository.GetAllPharmacyAsync());
        }

        public async Task<PharmacyResponseDTO> GetPharmacyByIdAsync(int id)
        {
            return _mapper.Map<PharmacyResponseDTO>(await _repository.GetPharmacyByIdAsync(id));
        }

        public async Task<int> UpdatePharmacyAsync(PharmacyRequestDTO pharmacyRequestDTO, int id)
        {
            var resultCheckingPharmasy = await _repository.GetPharmacyByIdAsync(id);
            if (resultCheckingPharmasy is not null)
            {
                resultCheckingPharmasy = _mapper.Map<Pharmacy>(pharmacyRequestDTO);
                resultCheckingPharmasy.PharmacyId = id;
                return await _repository.UpdatePharmacyAsync(resultCheckingPharmasy);
            }
            else
            {
                throw new Exception("Object cannot be updated");
            }
        }
    }
}
