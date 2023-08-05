using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;

namespace PharmacyShopping.BusinessLogic.Service.IServices
{
    public interface ICustomerService
    {
        Task<int> AddCustomerAsync(CustomerRequestDTO cutomerRequestDTO);

        Task<int> UpdateCustomerAsync(CustomerRequestDTO customerRequestDTO, int id);

        Task<int> DeleteCustomerAsync(int id);

        Task<CustomerResponseDTO> GetCustomerByIdAsync(int id);

        Task<List<CustomerResponseDTO>> GetAllCustomersAsync(string? searchWord);
    }
}