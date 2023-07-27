using PharmacyShopping.DataAccess.Models;

namespace PharmacyShopping.DataAccess.Repository.IRepositories
{
    public interface ISaleRepository
    {
        Task<int> AddSalesAsync(Sale sales);

        Task<int> UpdateSalesAsync(Sale sales);

        Task<int> DeleteSalesAsync(Sale sales);

        Task<List<Sale>> GetAllSalesAsync();

        Task<Sale> GetSalesByCustomerIdAsync(int id);
    }
}