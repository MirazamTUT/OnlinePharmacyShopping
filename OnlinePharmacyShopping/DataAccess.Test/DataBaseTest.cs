using PharmacyShopping.DataAccess.DbConnection;
using Moq;
using Xunit;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.Repositories;
using FluentAssertions;

namespace DataAccess.Test
{
    public class DataBaseTest
    {
        Mock<PharmacyDbContext> _dbContext = new Mock<PharmacyDbContext>();
        private readonly DataBaseRepository _repository;
        private readonly DataBase _dataBase;

        public DataBaseTest()
        {
            _repository = new DataBaseRepository(_dbContext.Object);
            _dataBase = new DataBase();
        }

        [Fact]
        public void WhenAddingNewDataBase_ReturnsInt()
        {
            var result = _repository.AddDataBase(_dataBase);
            result.Should().BeGreaterThanOrEqualTo(0);
        }
    }
}
