﻿using AutoMapper;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.Service.IServices;
using PharmacyShopping.DataAccess.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using PharmacyShopping.DataAccess.Models;

namespace PharmacyShopping.BusinessLogic.Service.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IMapper _mapper;
        private readonly IMedicineRepository _medicineRepository;

        public MedicineService(IMedicineRepository medicineRepository, IMapper mapper)
        {
            _medicineRepository = medicineRepository;
            _mapper = mapper;
        }
        
        public async Task<int> AddMedicineAsync(MedicineRequestDTO medicineRequestDTO)
        {
            try
            {
                return await _medicineRepository.AddMedicineAsync(_mapper.Map<Medicine>(medicineRequestDTO));
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Connection between database is failed");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was adding changes");
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
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation was failed wnet it was giving the info");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was giving medicines information");
            }
        }

        public async Task<List<MedicineResponseDTO>> GetMedicinesAsync()
        {
            try
            {
                return _mapper.Map<List<MedicineResponseDTO>>(await _medicineRepository.GetAllMedicinesAsync());
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation was failed wnet it was giving the info");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was giving medicines information");
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