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

        public PharmacyController(IPharmacyService pharmacyService, IValidator<PharmacyRequestDTO> validator)
        {
            _pharmacyService = pharmacyService;
            _validator = validator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddPharmacyAsync(PharmacyRequestDTO pharmacyRequestDto)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(pharmacyRequestDto);
                if (validationResult.IsValid)
                {
                    return await _pharmacyService.AddPharmacyAsync(pharmacyRequestDto);
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

        [HttpGet("Id")]
        public async Task<ActionResult<PharmacyResponseDTO>> GetPharmacyByIdAsync(int id)
        {
            try
            {
                return await _pharmacyService.GetPharmacyByIdAsync(id);
            }
            catch (AutoMapperMappingException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("All")]
        public async Task<ActionResult<List<PharmacyResponseDTO>>> GetAllPharmacyAsync()
        {
            try
            {
                return await _pharmacyService.GetAllPharmacyAsync();
            }
            catch (AutoMapperMappingException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<int>> UpdatePharmacyAsync(PharmacyRequestDTO pharmacyRequestDTO, int id)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(pharmacyRequestDTO);
                if (validationResult.IsValid)
                {
                    return await _pharmacyService.UpdatePharmacyAsync(pharmacyRequestDTO, id);
                }
                else
                {
                    throw new Exception("Pharmacy for update is not available.");
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

        [HttpDelete]
        public async Task<ActionResult<int>> DeletePharmacyAsync(int id)
        {
            try
            {
                return await _pharmacyService.DeletePharmacyAsync(id);
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