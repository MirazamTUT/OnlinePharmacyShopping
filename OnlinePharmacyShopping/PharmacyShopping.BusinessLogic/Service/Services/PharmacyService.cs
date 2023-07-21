using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;
using PharmacyShopping.BusinessLogic.Service.IServices;
using PharmacyShopping.DataAccess.Repository.IRepositories;
using PharmacyShopping.DataAccess.Models;

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
            try
            {
                return await _repository.AddPharmacyAsync(_mapper.Map<Pharmacy>(pharmacyRequestDto));
            }
            catch (AutoMapperMappingException ex)
            {
                throw new Exception("Mapping failed");
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Connection between database is failed");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }           
        }

        public async Task<int> DeletePharmacyAsync(int id)
        {
            try
            {
                var resultCheckingPharmacy = await _repository.GetPharmacyByIdAsync(id);
                if (resultCheckingPharmacy is not null)
                {
                    return await _repository.DeletePharmacyAsync(resultCheckingPharmacy);
                }
                else
                {
                    throw new Exception("Object cannot be deleted");
                }
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Connection between database is failed");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was deleting changes");
            }
        }

        public async Task<List<PharmacyResponseDTO>> GetAllPharmacyAsync()
        {
            try
            {
                return _mapper.Map<List<PharmacyResponseDTO>>(await _repository.GetAllPharmacyAsync());
            }
            catch (AutoMapperMappingException ex)
            {
                throw new Exception("Mapping failed");
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation was failed when it was giving the info");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PharmacyResponseDTO> GetPharmacyByIdAsync(int id)
        {
            try
            {
                return _mapper.Map<PharmacyResponseDTO>(await _repository.GetPharmacyByIdAsync(id));
            }
            catch (AutoMapperMappingException ex)
            {
                throw new Exception("Mapping failed");
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation was failed when it was giving the info");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdatePharmacyAsync(PharmacyRequestDTO pharmacyRequestDTO, int id)
        {
            try
            {
                var resultCheckingPharmacy = await _repository.GetPharmacyByIdAsync(id);
                if (resultCheckingPharmacy is not null)
                {
                    resultCheckingPharmacy = _mapper.Map<Pharmacy>(pharmacyRequestDTO);
                    resultCheckingPharmacy.PharmacyId = id;
                    return await _repository.UpdatePharmacyAsync(resultCheckingPharmacy);
                }
                else
                {
                    throw new Exception("Object cannot be updated");
                }
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Connection between database is failed");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it updating changes");
            }
        }
    }
}
