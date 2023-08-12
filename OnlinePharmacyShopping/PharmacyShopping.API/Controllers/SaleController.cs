using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;
using PharmacyShopping.BusinessLogic.Service.IServices;

namespace PharmacyShopping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISalesService _salesService;
        private readonly ILogger<SaleController> _logger;

        public SaleController(ISalesService salesService, ILogger<SaleController> logger)
        {
            _salesService = salesService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddSalesAsync(SaleRequestDTO salesRequestDTO)
        {
            try
            {
                _logger.LogInformation("Sales was successfully added.");
                return await _salesService.AddSalesAsync(salesRequestDTO);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error adding Sales to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error saving Sales to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpGet("Id")]
        public async Task<ActionResult<SaleResponseDTO>> GetSalesByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("SalesById was found successfully.");
                return await _salesService.GetSalesByIdAsync(id);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving SalesById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while retrieving SalesById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("All")]
        public async Task<ActionResult<List<SaleResponseDTO>>> GetAllSalesAsync()
        {
            try
            {
                _logger.LogInformation("All Sales were found successfully.");
                return await _salesService.GetAllSalesAsync();
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving all Sales in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is an error retrieving all Sales from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("Id")]
        public async Task<ActionResult<int>> UpdateSalesAsync(SaleRequestDTO salesRequestDTO, int id)
        {
            try
            {
                _logger.LogInformation("Sales was successfully updated.");
                return await _salesService.UpdateSalesAsync(salesRequestDTO, id);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while updating Sales {id} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while updating Sales {id} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpDelete("Id")]
        public async Task<ActionResult<int>> DeleteSalesAsync(int id)
        {
            try
            {
                _logger.LogInformation("Sales was successfully deleted.");
                return await _salesService.DeleteSalesAsync(id);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error deleting Sales to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error deleting Sales to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}