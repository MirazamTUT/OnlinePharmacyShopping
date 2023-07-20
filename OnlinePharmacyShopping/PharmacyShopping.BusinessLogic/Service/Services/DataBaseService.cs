using AutoMapper;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;
using PharmacyShopping.BusinessLogic.Service.IServices;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace PharmacyShopping.BusinessLogic.Service.Services
{
    public class DataBaseService : IDataBaseService
    {
        private readonly IDataBaseRepository _dataBaseRepository;
        private readonly IMapper _mapper;

        public DataBaseService(IDataBaseRepository dataBaseRepository, IMapper mapper)
        {
            _dataBaseRepository = dataBaseRepository;
            _mapper = mapper;
        }

        public async Task<int> AddDataBaseAsync(DataBase dataBase)
        {
            try
            {
                return await _dataBaseRepository.AddDataBaseAsync(_mapper.Map<DataBase>(dataBase));
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Connection between database is failed");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was adding changes");
            }
        }

        public async Task<DataBaseResponseDTO> GetDataBaseAsync()
        {
            try
            {
                return _mapper.Map<DataBaseResponseDTO>(await _dataBaseRepository.GetDataBaseAsync());
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation was failed when it was giving the info");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was giving DataBase information");
            }
        }
    }
}
