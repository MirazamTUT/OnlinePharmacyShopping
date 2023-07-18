using AutoMapper;
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

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<int> AddCustomerAsync(CustomerRequestDTO customerRequestDTO)
        {
            return await _customerRepository.AddCustomerAsync(_mapper.Map<Customer>(customerRequestDTO));
        }

        public async Task<int> DeleteCustomerAsync(CustomerRequestDTO customerRequestDTO, int id)
        {
            var customerResult = await _customerRepository.GetCustomerByIdAsync(id);
            if(customerResult is not null) 
            {
                return await _customerRepository.DeleteCustomerAsync(customerResult);
            }
            else
            {
                throw new Exception("Delete uchun Customer Topilmadi");
            }
        }

        public async Task<List<CustomerResponseDTO>> GetAllCustomersAsync()
        {
            return _mapper.Map<List<CustomerResponseDTO>>(await _customerRepository.GetAllCustomersAsync());
        }

        public async Task<CustomerResponseDTO> GetCustomerByIdAsync(int id)
        {
            return _mapper.Map<CustomerResponseDTO>(await _customerRepository.GetCustomerByIdAsync(id));
        }

        public async Task<int> UpdateCustomerAsync(CustomerRequestDTO customerRequestDTO, int id)
        {
            var customerResult = await _customerRepository.GetCustomerByIdAsync(id);
            if(customerResult is not null) 
            {
                customerResult = _mapper.Map<Customer>(customerRequestDTO);
                customerResult.CustomerId = id;
                return await _customerRepository.UpdateCustomerAsync(customerResult);
            }
            else
            {
                throw new Exception("Update uchun Customer mavjud emas");
            }
        }
    }
}
