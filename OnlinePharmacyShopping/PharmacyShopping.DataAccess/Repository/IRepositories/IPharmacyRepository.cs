﻿using PharmacyShopping.DataAccess.DbConnection;
using PharmacyShopping.DataAccess.Models;
using System;

namespace PharmacyShopping.DataAccess.Repository.IRepositories
{
    public interface IPharmacyRepository
    {
        Task<int> AddPharmacyAsync(Pharmacy pharmacy);
        
        Task<Pharmacy> GetPharmacyByIdAsync(int pharmacyId);
        
        Task<List<Pharmacy>> GetAllPharmacyAsync();
        
        Task<int> UpdatePharmacyAsync(Pharmacy pharmacy);
        
        Task<int> DeletePharmacyAsync(Pharmacy pharmacy);
    }
}