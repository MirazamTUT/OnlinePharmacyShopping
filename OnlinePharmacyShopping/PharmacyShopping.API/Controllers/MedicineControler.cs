using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;
using PharmacyShopping.BusinessLogic.Service.IServices;

namespace PharmacyShopping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineControler : ControllerBase
    {
        private readonly IMedicineService _medicineService;
        private readonly IValidator<MedicineRequestDTO> _validator;

        public MedicineControler(IMedicineService medicineService, IValidator<MedicineRequestDTO> validator)
        {
            _medicineService = medicineService;
            _validator = validator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddMedicine(MedicineRequestDTO medicineRequestDTO)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(medicineRequestDTO);
                if (validationResult.IsValid)
                {
                    return await _medicineService.AddMedicineAsync(medicineRequestDTO);
                }
                else
                {
                    throw new Exception("You entered the values incorrectly or incompletely, please try to enter them all correctly and completely again.");
                }
            }
            catch (AutoMapperMappingException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpGet("id")]
        public async Task<ActionResult<MedicineResponseDTO>> GetMedicineById(int id)
        {
            try
            {
                return await _medicineService.GetMedicineByIdAsync(id);
            }
            catch (AutoMapperMappingException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("MedicineId")]
        public async Task<ActionResult<List<int>>> GetAllReportMedicineByMedicineId(int id)
        {
            try
            {
                return await _medicineService.GetAllReportMedicineByMedicineIdAsync(id);
            }
            catch (AutoMapperMappingException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("All")]
        public async Task<ActionResult<List<MedicineResponseDTO>>> GetAllMedicine()
        {
            try
            {
                return await _medicineService.GetMedicinesAsync();
            }
            catch (AutoMapperMappingException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<int>> UpdateMedicine(MedicineRequestDTO medicineRequestDTO, int id)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(medicineRequestDTO);
                if (validationResult.IsValid)
                {
                    return await _medicineService.UpdateMedicineAsync(medicineRequestDTO, id);
                }
                else
                {
                    throw new Exception("Medicine for update is not available.");
                }
            }
            catch (AutoMapperMappingException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpDelete("id")]
        public async Task<ActionResult<int>> DeleteMedicine(int id)
        {
            try
            {
                return await _medicineService.DeleteMedicineAsync(id);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}