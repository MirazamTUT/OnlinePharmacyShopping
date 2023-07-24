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
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;
        private readonly IValidator<SalesRequestDTO> _validator;

        public SalesController(ISalesService salesService, IValidator<SalesRequestDTO> validator)
        {
            _salesService = salesService;
            _validator = validator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddSalesAsync(SalesRequestDTO salesRequestDTO)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(salesRequestDTO);
                if(validationResult.IsValid)
                {
                    return await _salesService.AddSalesAsync(salesRequestDTO);
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
        public async Task<ActionResult<SalesResponseDTO>> GetSalesByIdAsync(int id)
        {
            try
            {
                return await _salesService.GetSalesByIdAsync(id);
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
        public async Task<ActionResult<List<SalesResponseDTO>>> GetAllSalesAsync()
        {
            try
            {
                return await _salesService.GetAllSalesAsync();
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
        public async Task<ActionResult<int>> UpdateSalesAsync(SalesRequestDTO salesRequestDTO, int id)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(salesRequestDTO);
                if (validationResult.IsValid)
                {
                    return await _salesService.UpdateSalesAsync(salesRequestDTO, id);
                }
                else
                {
                    throw new Exception("Sales for update is not available.");
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
        public async Task<ActionResult<int>> DeleteSalesAsync(int id)
        {
            try
            {
                return await _salesService.DeleteSalesAsync(id);
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