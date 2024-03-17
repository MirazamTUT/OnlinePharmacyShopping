using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;
using PharmacyShopping.BusinessLogic.Service.IServices;
using System.Collections;

namespace PharmacyShopping.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IValidator<CustomerRequestDTO> _validator;
        private readonly IValidator<CustomerRequestDTOForLogin> _validatorForLogin;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerService customerService, IValidator<CustomerRequestDTO> validator, IValidator<CustomerRequestDTOForLogin> validatorForLogin, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _validator = validator;
            _logger = logger;
            _validatorForLogin = validatorForLogin;
        }

        [HttpPost("Register"), DisableRequestSizeLimit]
        public async Task<ActionResult<int>> RegisterAsync([FromForm] CustomerRequestDTO customerRequestDTO)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(customerRequestDTO);
                if (validationResult.IsValid)
                {
                    _logger.LogInformation("Customer was successfully added.");
                    return await _customerService.AddRegisterAsync(customerRequestDTO);
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
                _logger.LogError($"There is an error adding Customer to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error saving Customer to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> LoginAsync([FromForm] CustomerRequestDTOForLogin customerRequestDTOForLogin)
        {
            try
            {
                ValidationResult validationResult = await _validatorForLogin.ValidateAsync(customerRequestDTOForLogin);
                if (validationResult.IsValid)
                {
                    _logger.LogInformation("Customer was successfully added.");
                    return await _customerService.LoginAsync(customerRequestDTOForLogin);
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
                _logger.LogError($"There is an error adding Customer to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error saving Customer to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpGet("Id"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCustomerByIdAsync(int id)
        {
            try
            {
                var customerResponseDTO = await _customerService.GetCustomerByIdAsync(id);
                var content = customerResponseDTO.ContentOfImage;
                var contentType = customerResponseDTO.ContentType;
                customerResponseDTO.ContentOfImage = null;
                customerResponseDTO.ContentType = null;
                var Image = new MemoryStream(content);
                _logger.LogInformation("CustomerById was found successfully.");
                return customerResponseDTO;
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving Customer from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while retrieving Customer from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("All")]
        public async Task<ActionResult<List<CustomerResponseDTO>>> GetAllCustomersAsync(string? searchWord)
        {
            try
            {
                _logger.LogInformation("All Customers were found successfully.");
                return await _customerService.GetAllCustomersAsync(searchWord);
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving all Customers in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is an error retrieving all Customers from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("Id")]
        public async Task<ActionResult<int>> UpdateCustomerAsync(CustomerRequestDTO customerRequestDTO, int id)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(customerRequestDTO);
                if(validationResult.IsValid)
                {
                    _logger.LogInformation("Customer was successfully updated.");
                    return await _customerService.UpdateCustomerAsync(customerRequestDTO, id);
                }
                else
                {
                    throw new Exception("Customer for update is not available.");
                }
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while updating Customer {id} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while updating Customer {id} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        [HttpDelete("Id")]
        public async Task<ActionResult<int>> DeleteCustomerAsync(int id)
        {
            try
            {
                _logger.LogInformation("Customer was successfully deleted.");
                return await _customerService.DeleteCustomerAsync(id);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error deleting Customer to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error deleting Customer to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}