using PharmacyShopping.DataAccess.Models;

namespace PharmacyShopping.DataAccess.Repository.IRepositories
{
    public interface ICustomerRepository
    {
        Task<int> AddCustomerAsync(Customer customer);

        Task<int> UpdateCustomerAsync(Customer customer);

        Task<int> DeleteCustomerAsync(Customer customer);

        Task<Customer> GetCustomerByIdAsync(int id);

        Task<Customer> GetCustomerByFirstAndLastNamesAsync(string customerFirstName, string customerLastName);

        Task<List<Customer>> GetAllCustomersAsync(string? searchWord);
    }
}