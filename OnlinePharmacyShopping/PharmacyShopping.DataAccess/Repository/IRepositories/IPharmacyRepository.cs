using PharmacyShopping.DataAccess.Models;

namespace PharmacyShopping.DataAccess.Repository.IRepositories
{
    public interface IPharmacyRepository
    {
        Task<int> AddPharmacyAsync(Pharmacy pharmacy);
        
        Task<Pharmacy> GetPharmacyByIdAsync(int id);
        
        Task<List<Pharmacy>> GetAllPharmacyAsync();
        
        Task<int> UpdatePharmacyAsync(Pharmacy pharmacy);
        
        Task<int> DeletePharmacyAsync(Pharmacy pharmacy);
    }
}