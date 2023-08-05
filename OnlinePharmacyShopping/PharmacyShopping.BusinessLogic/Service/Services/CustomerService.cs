using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;
using PharmacyShopping.BusinessLogic.Service.IServices;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.IRepositories;

namespace PharmacyShopping.BusinessLogic.Service.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper, ILogger<CustomerService> logger)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> AddCustomerAsync(CustomerRequestDTO customerRequestDTO)
        {
            try
            {
                _logger.LogInformation("Customer was successfully added.");
                return await _customerRepository.AddCustomerAsync(_mapper.Map<Customer>(customerRequestDTO));
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new Exception("Mapping failed.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error adding Customer to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error saving Customer to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> DeleteCustomerAsync(int id)
        {
            try
            {
                var customerResult = await _customerRepository.GetCustomerByIdAsync(id);
                if (customerResult is not null)
                {
                    _logger.LogInformation("Customer was successfully deleted.");
                    return await _customerRepository.DeleteCustomerAsync(customerResult);
                }
                else
                {
                    throw new Exception("Object cannot be deleted.");
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error deleting Customer to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error deleting Customer to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was deleting changes.");
            }
        }

        public async Task<List<CustomerResponseDTO>> GetAllCustomersAsync(string? searchWord)
        {
            try
            {
                _logger.LogInformation("All Customers were found successfully.");
                return _mapper.Map<List<CustomerResponseDTO>>(await _customerRepository.GetAllCustomersAsync(searchWord));
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new Exception("Mapping failed");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving all Customers in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is an error retrieving all Customers from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }       
        }

        public async Task<CustomerResponseDTO> GetCustomerByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("CustomerById was found successfully.");
                return _mapper.Map<CustomerResponseDTO>(await _customerRepository.GetCustomerByIdAsync(id));
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new Exception("Mapping failed.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving CustomerById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while retrieving CustomerById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }         
        }

        public async Task<int> UpdateCustomerAsync(CustomerRequestDTO customerRequestDTO, int id)
        {
            try
            {
                var customerResult = await _customerRepository.GetCustomerByIdAsync(id);
                if (customerResult is not null)
                {
                    customerResult = _mapper.Map<Customer>(customerRequestDTO);
                    customerResult.CustomerId = id;
                    _logger.LogInformation("Customer was successfully updated.");
                    return await _customerRepository.UpdateCustomerAsync(customerResult);
                }
                else
                {
                    throw new Exception("Object cannot be updated.");
                }
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new Exception("Mapping failed.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while updating Customer {id} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while updating Customer {id} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was updating changes.");
            }
        }
    }
}