using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.Service.IServices;
using PharmacyShopping.DataAccess.Repository.IRepositories;
using PharmacyShopping.DataAccess.Models;

namespace PharmacyShopping.BusinessLogic.Service.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IMapper _mapper;
        private readonly IMedicineRepository _medicineRepository;
        private readonly IReportMedicineRepository _reportMedicineRepository;

        public MedicineService(IMedicineRepository medicineRepository, IMapper mapper, IReportMedicineRepository reportMedicineRepository)
        {
            _medicineRepository = medicineRepository;
            _mapper = mapper;
            _reportMedicineRepository = reportMedicineRepository;
        }

        public async Task<int> AddMedicineAsync(MedicineRequestDTO medicineRequestDTO)
        {
            try
            {
                return await _medicineRepository.AddMedicineAsync(_mapper.Map<Medicine>(medicineRequestDTO));
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

        public async Task<int> DeleteMedicineAsync(int medicineId)
        {
            try
            {
                var medicineResult = await _medicineRepository.GetMedicineByIdAsync(medicineId);
                if(medicineResult is not null)
                {
                    return await _medicineRepository.DeleteMedicineAsync(medicineResult);
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

        public async Task<MedicineResponseDTO> GetMedicineByIdAsync(int medicineId)
        {
            try
            {
                return _mapper.Map<MedicineResponseDTO>(await _medicineRepository.GetMedicineByIdAsync(medicineId));
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

        public async Task<List<MedicineResponseDTO>> GetMedicinesAsync()
        {
            try
            {
                return _mapper.Map<List<MedicineResponseDTO>>(await _medicineRepository.GetAllMedicinesAsync());
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
                return resultOfReportIds;
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

        public async Task<int> UpdateMedicineAsync(MedicineRequestDTO medicineRequestDTO, int medicineId)
        {
            try
            {
                var medicineResult = await _medicineRepository.GetMedicineByIdAsync(medicineId);
                if(medicineResult is not null)
                {
                    medicineResult = _mapper.Map<Medicine>(medicineRequestDTO);
                    medicineResult.MedicineId = medicineId;
                    return await _medicineRepository.UpdateMedicineAsync(medicineResult);
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
