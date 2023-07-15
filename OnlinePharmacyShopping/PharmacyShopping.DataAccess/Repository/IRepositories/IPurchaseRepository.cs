using PharmacyShopping.DataAccess.Models;

namespace PharmacyShopping.DataAccess.Repository.IRepositories
{
    public interface IPurchaseRepository
    {
        Task<int> AddPurchaseAsync(Purchase purchase);

        Task<int> UpdatePurchaseAsync(Purchase purchase);

        Task<int> DeletePurchaseAsync(Purchase purchase);

        Task<Purchase> GetPurchaseByIdAsync(int id);

        Task<List<Purchase>> GetAllPurchasesAsync();
    }
}
