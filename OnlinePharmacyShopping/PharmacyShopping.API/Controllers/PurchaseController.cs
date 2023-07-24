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
        private readonly ILogger<PurchaseController> _logger;

        public PurchaseController(IPurchaseService purchaseService, IValidator<PurchaseRequestDTO> validator, ILogger<PurchaseController> logger)
        {
            _purchaseService = purchaseService;
            _validator = validator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddPurchase(PurchaseRequestDTO purchaseRequestDTO)
        {
            try
            {
               ValidationResult validationResult = await _validator.ValidateAsync(purchaseRequestDTO);
                if(validationResult.IsValid)
                {
                    _logger.LogInformation("Purchase was successfully added.");
                    return await _purchaseService.AddPurchaseAsync(purchaseRequestDTO);
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
                _logger.LogError($"There is an error adding Purchase to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error saving Purchase to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpGet("Id")]
        public async Task<ActionResult<PurchaseResponseDTO>> GetPurchaseById(int id)
        {
            try
            {
                _logger.LogInformation("PurchaseById was found successfully.");
                return await _purchaseService.GetPurchaseByIdAsync(id);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while retrieving PurchaseById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while retrieving PurchaseById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("All")]
        public async Task<ActionResult<List<PurchaseResponseDTO>>> GetAllPurchase()
        {
            try
            {
                _logger.LogInformation("All Purchases were found successfully.");
                return await _purchaseService.GetAllPurchasesAsync();
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while retrieving all Purchases in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is an error retrieving all Purchases from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("Id")]
        public async Task<ActionResult<int>> DeletePurchase(int id)
        {
            try
            {
                _logger.LogInformation("Purchase was successfully deleted.");
                return await _purchaseService.DeletePurchaseAsync(id);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error deleting Purchase to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error deleting Purchase to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPut("Id")]
        public async Task<ActionResult<int>> UpdatePurchase(PurchaseRequestDTO purchaseRequestDTO, int id)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(purchaseRequestDTO);
                if (validationResult.IsValid)
                {
                    _logger.LogInformation("Purchase was successfully updated.");
                    return await _purchaseService.UpdatePurchaseAsync(purchaseRequestDTO, id);
                }
                else
                {
                    throw new Exception("Purchase for update is not available.");
                }
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while updating Purchase {id} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while updating Purchase {id} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}