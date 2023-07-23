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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IValidator<CustomerRequestDTO> _validator;

        public CustomerController(ICustomerService customerService, IValidator<CustomerRequestDTO> validator)
        {
            _customerService = customerService;
            _validator = validator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddCustomerAsync(CustomerRequestDTO customerRequestDTO)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(customerRequestDTO);
                if (validationResult.IsValid)
                {
                    return await _customerService.AddCustomerAsync(customerRequestDTO);
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
        public async Task<ActionResult<CustomerResponseDTO>> GetCustomerByIdAsync(int id)
        {
            try
            {
                return await _customerService.GetCustomerByIdAsync(id);
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
        public async Task<ActionResult<List<CustomerResponseDTO>>> GetAllCustomersAsync()
        {
            try
            {
                return await _customerService.GetAllCustomersAsync();
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
        public async Task<ActionResult<int>> UpdateCustomerAsync(CustomerRequestDTO customerRequestDTO, int id)
        {
            try
            {
                ValidationResult validationResult = await _validator.ValidateAsync(customerRequestDTO);
                if(validationResult.IsValid)
                {
                    return await _customerService.UpdateCustomerAsync(customerRequestDTO, id);
                }
                else
                {
                    throw new Exception("Customer for update is not available.");
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
        public async Task<ActionResult<int>> DeleteCustomerAsync(int id)
        {
            try
            {
                return await _customerService.DeleteCustomerAsync(id);
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