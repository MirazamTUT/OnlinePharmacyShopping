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
    public class PharmacyController : ControllerBase
    {
        private readonly IPharmacyService _pharmacyService;
        private readonly IValidator<PharmacyRequestDTO> _validator;
        private readonly ILogger<PharmacyController> _logger;

        public PharmacyController(IPharmacyService pharmacyService, IValidator<PharmacyRequestDTO> validator, ILogger<PharmacyController> logger)
        {
            _pharmacyService = pharmacyService;
            _validator = validator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddPharmacyAsync(PharmacyRequestDTO pharmacyRequestDto)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(pharmacyRequestDto);
                if (validationResult.IsValid)
                {
                    _logger.LogInformation("Pharmacy was successfully added.");
                    return await _pharmacyService.AddPharmacyAsync(pharmacyRequestDto);
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
                _logger.LogError($"There is an error adding Pharmacy to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error saving Pharmacy to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpGet("Id")]
        public async Task<ActionResult<PharmacyResponseDTO>> GetPharmacyByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("PharmacyById was found successfully.");
                return await _pharmacyService.GetPharmacyByIdAsync(id);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving PharmacyById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while retrieving PharmacyById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("All")]
        public async Task<ActionResult<List<PharmacyResponseDTO>>> GetAllPharmacyAsync()
        {
            try
            {
                _logger.LogInformation("All Pharmacys were found successfully.");
                return await _pharmacyService.GetAllPharmacyAsync();
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving all Pharmacys in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is an error retrieving all Pharmacys from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("Id")]
        public async Task<ActionResult<int>> UpdatePharmacyAsync(PharmacyRequestDTO pharmacyRequestDTO, int id)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(pharmacyRequestDTO);
                if (validationResult.IsValid)
                {
                    _logger.LogInformation("Pharmacy was successfully updated.");
                    return await _pharmacyService.UpdatePharmacyAsync(pharmacyRequestDTO, id);
                }
                else
                {
                    throw new Exception("Pharmacy for update is not available.");
                }
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while updating Pharmacy {id} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while updating Pharmacy {id} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpDelete("Id")]
        public async Task<ActionResult<int>> DeletePharmacyAsync(int id)
        {
            try
            {
                _logger.LogInformation("Pharmacy was successfully deleted.");
                return await _pharmacyService.DeletePharmacyAsync(id);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error deleting Pharmacy to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error deleting Pharmacy to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}