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

        public async Task<int> DeleteDataBaseAsync(int id)
        {
            try
            {
                var resultDatabase = await _dataBaseRepository.GetDataBaseAsync();
                if (resultDatabase is not null) 
                {
                    return await _dataBaseRepository.DeleteDataBaseAsync(resultDatabase);
                }
                else
                {
                    throw new Exception("Object didn't find for deleting");
                }
            }
            catch(DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
            catch(Exception ex)
            {
                throw new Exception("Operation was failed when it was deleting changes");
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
                throw new Exception("Operation was failed wnet it was giving the info");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was giving DataBase information");
            }
        }

        public async Task<int> UpdateDataBaseAsync(DataBase dataBase, int id)
        {
            try
            {
                var resultDataBase = await _dataBaseRepository.GetDataBaseAsync();
                if (resultDataBase is not null)
                {
                    resultDataBase = _mapper.Map<DataBase>(dataBase);
                    resultDataBase.DataBaseId = id;
                    return await _dataBaseRepository.UpdateDataBaseAsync(resultDataBase);
                }
                else
                {
                    throw new Exception("Object didn't find for updating");
                }
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
            catch(Exception ex)
            {
                throw new Exception("Operation was failed when it was Updating DataBase information");
            }
        }
    }
}
