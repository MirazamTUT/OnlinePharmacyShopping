using PharmacyShopping.DataAccess.Models;

namespace PharmacyShopping.DataAccess.Repository.IRepositories
{
    public interface ISalesRepository
    {
        Task<int> AddSalesAsync(Sales sales);

        Task<int> UpdateSalesAsync(Sales sales);

        Task<int> DeleteSalesAsync(Sales sales);

        Task<List<Sales>> GetAllSalesAsync();

        Task<Sales> GetSalesByCustomerIdAsync(int id);
    }
}
