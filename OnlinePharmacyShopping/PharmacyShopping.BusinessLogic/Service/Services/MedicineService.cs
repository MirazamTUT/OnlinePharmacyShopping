using AutoMapper;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.Service.IServices;
using PharmacyShopping.DataAccess.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using PharmacyShopping.DataAccess.Models;
using Microsoft.Extensions.Logging;

namespace PharmacyShopping.BusinessLogic.Service.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IMapper _mapper;
        private readonly IMedicineRepository _medicineRepository;
        private readonly IReportMedicineRepository _reportMedicineRepository;
        private readonly ILogger<MedicineService> _logger;

        public MedicineService(IMedicineRepository medicineRepository, IMapper mapper, ILogger<MedicineService> logger, IReportMedicineRepository reportMedicineRepository)
        {
            _medicineRepository = medicineRepository;
            _mapper = mapper;
            _reportMedicineRepository = reportMedicineRepository;
            _logger = logger;
        }

        public async Task<int> AddMedicineAsync(MedicineRequestDTO medicineRequestDTO)
        {
            try
            {
                _logger.LogInformation("Medicine was successfully added.");
                return await _medicineRepository.AddMedicineAsync(_mapper.Map<Medicine>(medicineRequestDTO));
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new Exception("Mapping failed.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error adding Medicine to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error saving Medicine to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> DeleteMedicineAsync(int medicineId)
        {
            try
            {
                var medicineResult = await _medicineRepository.GetMedicineByIdAsync(medicineId);
                if(medicineResult is not null)
                {
                    _logger.LogInformation("Medicine was successfully deleted.");
                    return await _medicineRepository.DeleteMedicineAsync(medicineResult);
                }
                else
                {
                    throw new Exception("Object cannot be deleted.");
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error deleting Medicine to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error deleting Medicine to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was deleting changes.");
            }
        }

        public async Task<MedicineResponseDTO> GetMedicineByIdAsync(int medicineId)
        {
            try
            {
                _logger.LogInformation("MedicineById was found successfully.");
                return _mapper.Map<MedicineResponseDTO>(await _medicineRepository.GetMedicineByIdAsync(medicineId));
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new Exception("Mapping failed.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving MedicineById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while retrieving MedicineById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<MedicineResponseDTO>> GetMedicinesAsync()
        {
            try
            {
                _logger.LogInformation("All Medicines were found successfully.");
                return _mapper.Map<List<MedicineResponseDTO>>(await _medicineRepository.GetAllMedicinesAsync());
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new Exception("Mapping failed.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving all Medicines in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is an error retrieving all Medicines from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<int>> GetAllReportMedicineByMedicineIdAsync(int id)
        {
            try
            {
                var resultOfMedicine = await _medicineRepository.GetMedicineByIdAsync(id);
                var resultOfReportMedicines = await _reportMedicineRepository.GetAllReportMedicineByMedicineIdAsync(id);
                var resultOfReportIds = new List<int>();
                foreach(ReportMedicine reportMedicine in resultOfReportMedicines)
                {
                    resultOfReportIds.Add(reportMedicine.ReportId);
                }
                _logger.LogInformation("All ReportMedicineByMedicineIds were found successfully.");
                return resultOfReportIds;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving all ReportMedicineByMedicineIds in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is an error retrieving all ReportMedicineByMedicineIds from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was giving reports information");
            }
        }

        public async Task<int> UpdateMedicineAsync(MedicineRequestDTO medicineRequestDTO, int medicineId)
        {
            try
            {
                var medicineResult = await _medicineRepository.GetMedicineByIdAsync(medicineId);
                if(medicineResult is not null)
                {
                    medicineResult = _mapper.Map<Medicine>(medicineRequestDTO);
                    medicineResult.MedicineId = medicineId;
                    _logger.LogInformation("Medicine was successfully updated.");
                    return await _medicineRepository.UpdateMedicineAsync(medicineResult);
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
                _logger.LogError($"An error occurred while updating Medicine {medicineId} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while updating Medicine {medicineId} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when was it updating changes.");
            }
        }
    }
}