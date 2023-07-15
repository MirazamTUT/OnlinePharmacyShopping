using PharmacyShopping.DataAccess.Models;

namespace PharmacyShopping.DataAccess.Repository.IRepositories
{
    public interface ISalesRepository
    {
        Task<int> AddSales(Sales sales);

        Task<int> UpdateSales(Sales sales);

        Task<int> DeleteSales(Sales sales);

        Task<List<Sales>> GetAllSales();

        Task<Sales> GetSalesByCustomerId(int id);
    }
}
