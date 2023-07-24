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
    public class DataBaseController : ControllerBase
    {
        private readonly IDataBaseService _dataBaseService;
        private readonly ILogger<DataBaseController> _logger;

        public DataBaseController(IDataBaseService dataBaseService, ILogger<DataBaseController> logger)
        {
            _dataBaseService = dataBaseService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddDataBase(DataBaseRequestDTO dataBaseRequestDTO)
        {
            try
            {
                _logger.LogInformation("Datas was successfully added.");
                return await _dataBaseService.AddDataBaseAsync(dataBaseRequestDTO);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error adding Datas to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error saving Datas to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpGet("All")]
        public async Task<ActionResult<DataBaseResponseDTO>> GetDataBase()
        {
            try
            {
                _logger.LogInformation("All Datas were found successfully.");
                return await _dataBaseService.GetDataBaseAsync();
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while retrieving all Datas in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is an error retrieving all Datas from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("Id")]
        public async Task<ActionResult<int>> DeleteDataBase(int id)
        {
            try
            {
                _logger.LogInformation("Datas was successfully deleted.");
                return await _dataBaseService.DeleteDataBaseAsync(id);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error deleting Datas to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error deleting Datas to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPut("Id")]
        public async Task<ActionResult<int>> UpdateDataBase(DataBaseRequestDTO dataBaseRequestDTO, int id)
        {
            try
            {
                _logger.LogInformation("Datas was successfully updated.");
                return await _dataBaseService.UpdateDataBaseAsync(dataBaseRequestDTO, id);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while updating Datas {id} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while updating Datas {id} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}