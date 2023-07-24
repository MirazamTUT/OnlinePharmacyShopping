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
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IValidator<PurchaseRequestDTO> _validator;

        public PurchaseController(IPurchaseService purchaseService, IValidator<PurchaseRequestDTO> validator)
        {
            _purchaseService = purchaseService;
            _validator = validator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddPurchase(PurchaseRequestDTO purchaseRequestDTO)
        {
            try
            {
               ValidationResult validationResult = await _validator.ValidateAsync(purchaseRequestDTO);
                if(validationResult.IsValid)
                {
                    return await _purchaseService.AddPurchaseAsync(purchaseRequestDTO);
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
        public async Task<ActionResult<PurchaseResponseDTO>> GetPurchaseById(int id)
        {
            try
            {
                return await _purchaseService.GetPurchaseByIdAsync(id);
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
        public async Task<ActionResult<List<PurchaseResponseDTO>>> GetAllPurchase()
        {
            try
            {
                return await _purchaseService.GetAllPurchasesAsync();
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

        [HttpDelete("id")]
        public async Task<ActionResult<int>> DeletePurchase(int id)
        {
            try
            {
                return await _purchaseService.DeletePurchaseAsync(id);
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

        [HttpPut("id")]
        public async Task<ActionResult<int>> UpdatePurchase(PurchaseRequestDTO purchaseRequestDTO, int id)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(purchaseRequestDTO);
                if (validationResult.IsValid)
                {
                    return await _purchaseService.UpdatePurchaseAsync(purchaseRequestDTO, id);
                }
                else
                {
                    throw new Exception("Purchase for update is not available.");
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
    }
}