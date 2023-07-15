using Microsoft.EntityFrameworkCore;
using PharmacyShopping.DataAccess.DbConnection;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.Repositories;
using Xunit;
namespace PharmacyShopping.DataAccess.Repository.Tests
{
    public class PharmacyRepositoryTests
    {
        private readonly PharmacyDbContext _context;
        private readonly PharmacyRepository _repository;
        public PharmacyRepositoryTests()
        {
            _context = new PharmacyDbContext();
            _repository= new PharmacyRepository(_context);
        }

        [Fact]
        public async Task AddPharmacy_ShouldReturnThePharmacyId()
        {
            var pharmacy = new Pharmacy
            {
                PharmacyName = "My Pharmacy",
                //City = "Los Angeles"
            };

            var pharmacyId = await _repository.AddPharmacy(pharmacy);

            Assert.Equal(pharmacyId, pharmacy.PharmacyId);
        }

        [Fact]
        public async Task DeletePharmacy_ShouldDeleteThePharmacy()
        {
            var pharmacy = new Pharmacy
            {
                PharmacyName = "My Pharmacy",
                //City = "Los Angeles"
            };

            var pharmacyId = await _repository.AddPharmacy(pharmacy);

            await _repository.DeletePharmacy(pharmacy);

            var pharmacyInDb = await _context.Pharmacies.FirstOrDefaultAsync(x => x.PharmacyId == pharmacyId);

            Assert.Null(pharmacyInDb);
        }

        [Fact]
        public async Task GetAllPharmacy_ShouldReturnAllPharmacies()
        {
            var pharmacy1 = new Pharmacy
            {
                PharmacyName = "My Pharmacy 1",
                //City = "Los Angeles"
            };

            var pharmacy2 = new Pharmacy
            {
                PharmacyName = "My Pharmacy 2",
                //City = "San Francisco"
            };

            await _repository.AddPharmacy(pharmacy1);
            await _repository.AddPharmacy(pharmacy2);

            var pharmacies = await _repository.GetAllPharmacy();

            Assert.Equal(2, pharmacies.Count);
            Assert.Contains(pharmacy1, pharmacies);
            Assert.Contains(pharmacy2, pharmacies);
        }

        [Fact]
        public async Task GetPharmacyById_ShouldReturnThePharmacy()
        {
            var pharmacy = new Pharmacy
            {PharmacyName = "My Pharmacy"
            };

            var pharmacyId = await _repository.AddPharmacy(pharmacy);

            var pharmacyInDb = await _repository.GetPharmacyById(pharmacyId);

            Assert.Equal(pharmacy, pharmacyInDb);
        }

        [Fact]
        public async Task UpdatePharmacy_ShouldUpdateThePharmacy()
        {
            var pharmacy = new Pharmacy
            {
                PharmacyName = "My Pharmacy"
            };

            var pharmacyId = await _repository.AddPharmacy(pharmacy);

            pharmacy.PharmacyName = "My New Pharmacy";
            //pharmacy.City = "San Francisco";

            await _repository.UpdatePharmacy(pharmacy);

            var pharmacyInDb = await _context.Pharmacies.FirstOrDefaultAsync(x => x.PharmacyId == pharmacyId);

            Assert.Equal(pharmacy.PharmacyName, pharmacyInDb.PharmacyName);
            //Assert.Equal(pharmacy.City, pharmacyInDb.City);
        }
    }
}
