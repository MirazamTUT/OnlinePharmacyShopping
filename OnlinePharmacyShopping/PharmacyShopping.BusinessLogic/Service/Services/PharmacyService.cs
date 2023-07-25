using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<PharmacyService> _logger;

        public PharmacyService(IPharmacyRepository repository, IMapper mapper, ILogger<PharmacyService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> AddPharmacyAsync(PharmacyRequestDTO pharmacyRequestDto)
        {
            try
            {
                _logger.LogInformation("Pharmacy was successfully added.");
                return await _repository.AddPharmacyAsync(_mapper.Map<Pharmacy>(pharmacyRequestDto));
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new Exception("Mapping failed.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error adding Pharmacy to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error saving Pharmacy to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
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
                    _logger.LogInformation("Pharmacy was successfully deleted.");
                    return await _repository.DeletePharmacyAsync(resultCheckingPharmacy);
                }
                else
                {
                    throw new Exception("Object cannot be deleted.");
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error deleting Pharmacy to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error deleting Pharmacy to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was deleting changes.");
            }
        }

        public async Task<List<PharmacyResponseDTO>> GetAllPharmacyAsync()
        {
            try
            {
                _logger.LogInformation("All Pharmacys were found successfully.");
                return _mapper.Map<List<PharmacyResponseDTO>>(await _repository.GetAllPharmacyAsync());
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new Exception("Mapping failed.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving all Pharmacys in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is an error retrieving all Pharmacys from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
        }

        public async Task<PharmacyResponseDTO> GetPharmacyByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("PharmacyById was found successfully.");
                return _mapper.Map<PharmacyResponseDTO>(await _repository.GetPharmacyByIdAsync(id));
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new Exception("Mapping failed.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving PharmacyById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while retrieving PharmacyById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
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
                    _logger.LogInformation("Pharmacy was successfully updated.");
                    return await _repository.UpdatePharmacyAsync(resultCheckingPharmacy);
                }
                else
                {
                    throw new Exception("Object cannot be updated.");
                }
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new Exception("Mapping failed.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while updating Pharmacy {id} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while updating Pharmacy {id} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was updating changes.");
            }
        }
    }
}