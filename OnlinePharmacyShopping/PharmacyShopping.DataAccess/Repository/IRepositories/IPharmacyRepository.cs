using PharmacyShopping.DataAccess.DbConnection;
using PharmacyShopping.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShopping.DataAccess.Repository.IRepositories
{
    public interface IPharmacyRepository
    {
        Task<int> AddPharmacy(Pharmacy pharmacy);
        Task<Pharmacy> GetPharmacyById(int pharmacyId);
        Task<List<Pharmacy>> GetAllPharmacy();
        Task<int> UpdatePharmacy(Pharmacy pharmacy);
        Task<int> DeletePharmacy(Pharmacy pharmacy);
    }
}
