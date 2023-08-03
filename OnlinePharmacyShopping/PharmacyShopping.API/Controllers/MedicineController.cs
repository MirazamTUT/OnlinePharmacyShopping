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
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _medicineService;
        private readonly IValidator<MedicineRequestDTO> _validator;
        private readonly ILogger<MedicineController> _logger;

        public MedicineController(IMedicineService medicineService, IValidator<MedicineRequestDTO> validator, ILogger<MedicineController> logger)
        {
            _medicineService = medicineService;
            _validator = validator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddMedicine(MedicineRequestDTO medicineRequestDTO)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(medicineRequestDTO);
                if (validationResult.IsValid)
                {
                    _logger.LogInformation("Medicine was successfully added.");
                    return await _medicineService.AddMedicineAsync(medicineRequestDTO);
                }
                else
                {
                    throw new Exception("You entered the values incorrectly or incompletely, please try to enter them all correctly and completely again.");
                }
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error adding Medicine to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error saving Medicine to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpGet("Id")]
        public async Task<ActionResult<MedicineResponseDTO>> GetMedicineById(int id)
        {
            try
            {
                _logger.LogInformation("MedicineById was found successfully.");
                return await _medicineService.GetMedicineByIdAsync(id);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while retrieving MedicineById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while retrieving MedicineById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("MedicineId")]
        public async Task<ActionResult<List<int>>> GetAllReportMedicineByMedicineId(int id)
        {
            try
            {
                _logger.LogInformation("All ReportMedicineByMedicineIds were found successfully.");
                return await _medicineService.GetAllReportMedicineByMedicineIdAsync(id);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while retrieving all ReportMedicineByMedicineIds in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is an error retrieving all ReportMedicineByMedicineIds from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("All")]
        public async Task<ActionResult<List<MedicineResponseDTO>>> GetAllMedicine(string? searchWord)
        {
            try
            {
                return await _medicineService.GetMedicinesAsync(searchWord);
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

        [HttpPut("Id")]
        public async Task<ActionResult<int>> UpdateMedicine(MedicineRequestDTO medicineRequestDTO, int id)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(medicineRequestDTO);
                if (validationResult.IsValid)
                {
                    _logger.LogInformation("Medicine was successfully updated.");
                    return await _medicineService.UpdateMedicineAsync(medicineRequestDTO, id);
                }
                else
                {
                    throw new Exception("Medicine for update is not available.");
                }
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while updating Medicine {id} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while updating Medicine {id} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpDelete("Id")]
        public async Task<ActionResult<int>> DeleteMedicine(int id)
        {
            try
            {
                _logger.LogInformation("Medicine was successfully deleted.");
                return await _medicineService.DeleteMedicineAsync(id);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error deleting Medicine to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error deleting Medicine to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}