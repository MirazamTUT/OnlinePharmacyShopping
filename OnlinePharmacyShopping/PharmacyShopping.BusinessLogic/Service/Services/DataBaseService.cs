using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;
using PharmacyShopping.BusinessLogic.Service.IServices;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.IRepositories;

namespace PharmacyShopping.BusinessLogic.Service.Services
{
    public class DataBaseService : IDataBaseService
    {
        private readonly IDataBaseRepository _dataBaseRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DataBaseService> _logger;

        public DataBaseService(IDataBaseRepository dataBaseRepository, IMapper mapper, ILogger<DataBaseService> logger)
        {
            _dataBaseRepository = dataBaseRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> AddDataBaseAsync(DataBaseRequestDTO dataBaseRequestDTO)
        {
            try
            {
                _logger.LogInformation("Datas was successfully added.");
                return await _dataBaseRepository.AddDataBaseAsync(_mapper.Map<DataBase>(dataBaseRequestDTO));
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new Exception("Mapping failed.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error adding Datas to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error saving Datas to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> DeleteDataBaseAsync(int id)
        {
            try
            {
                var resultDatabase = await _dataBaseRepository.GetDataBaseAsync();
                if (resultDatabase is not null)
                {
                    _logger.LogInformation("Datas was successfully deleted.");
                    return await _dataBaseRepository.DeleteDataBaseAsync(resultDatabase);
                }
                else
                {
                    throw new Exception("Object didn't find for deleting.");
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error deleting Datas to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error deleting Datas to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was deleting changes.");
            }
        }

        public async Task<DataBaseResponseDTO> GetDataBaseAsync()
        {
            try
            {
                _logger.LogInformation("All Datas were found successfully.");
                return _mapper.Map<DataBaseResponseDTO>(await _dataBaseRepository.GetDataBaseAsync());
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new Exception("Mapping failed.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving all Datas in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is an error retrieving all Datas from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdateDataBaseAsync(DataBaseRequestDTO dataBaseRequestDTO, int id)
        {
            try
            {
                var resultDataBase = await _dataBaseRepository.GetDataBaseAsync();
                if (resultDataBase is not null)
                {
                    resultDataBase = _mapper.Map<DataBase>(dataBaseRequestDTO);
                    resultDataBase.DataBaseId = id;
                    _logger.LogInformation("Datas was successfully updated.");
                    return await _dataBaseRepository.UpdateDataBaseAsync(resultDataBase);
                }
                else
                {
                    throw new Exception("Object didn't find for updating.");
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while updating Datas {id} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while updating Datas {id} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was Updating DataBase information.");
            }
        }
    }
}