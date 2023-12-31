﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;
using PharmacyShopping.BusinessLogic.Service.IServices;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.IRepositories;

namespace PharmacyShopping.BusinessLogic.Service.Services
{
    public class SalesService : ISalesService
    {
        private readonly ISaleRepository _salesRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SalesService> _logger;

        public SalesService(ISaleRepository salesRepository, ILogger<SalesService> logger, IMapper mapper)
        {
            _salesRepository = salesRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> AddSalesAsync(SaleRequestDTO salesRequestDTO)
        {
            try
            {
                _logger.LogInformation("Sales was successfully added.");
                return await _salesRepository.AddSalesAsync(_mapper.Map<Sale>(salesRequestDTO));
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new Exception("Mapping failed.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error adding Sales to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error saving Sales to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> DeleteSalesAsync(int id)
        {
            try
            {
                var salesResult = await _salesRepository.GetSalesByIdAsync(id);
                if (salesResult is not null)
                {
                    _logger.LogInformation("Sales was successfully deleted.");
                    return await _salesRepository.DeleteSalesAsync(salesResult);
                }
                else
                {
                    throw new Exception("Object cannot be deleted.");
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"There is an error deleting Sales to the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error deleting Sales to database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was deleting changes.");
            }
        }

        public async Task<List<SaleResponseDTO>> GetAllSalesAsync()
        {
            try
            {
                _logger.LogInformation("All Sales were found successfully.");
                return _mapper.Map<List<SaleResponseDTO>>(await _salesRepository.GetAllSalesAsync());
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new Exception("Mapping failed.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving all Sales in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There is an error retrieving all Sales from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
        }

        public async Task<SaleResponseDTO> GetSalesByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("SalesById was found successfully.");
                return _mapper.Map<SaleResponseDTO>(await _salesRepository.GetSalesByIdAsync(id));
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new Exception("Mapping failed.");
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"An error occurred while retrieving SalesById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while retrieving SalesById from the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdateSalesAsync(SaleRequestDTO salesRequestDTO, int id)
        {
            try
            {
                var salesResult = await _salesRepository.GetSalesByIdAsync(id);
                if (salesResult is not null)
                {
                    salesResult = _mapper.Map<Sale>(salesRequestDTO);
                    salesResult.SaleId = id;
                    _logger.LogInformation("Sales was successfully updated.");
                    return await _salesRepository.UpdateSalesAsync(salesResult);
                }
                else
                {
                    throw new Exception("Object cannot be updated.");
                }
            }
            catch (AutoMapperMappingException ex)
            {
                _logger.LogError($"Mapping failed: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw new Exception("Mapping failed.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"An error occurred while updating Sales {id} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while updating Sales {id} in the database: {ex.Message}, StackTrace: {ex.StackTrace}.");
                throw new Exception("Operation was failed when it was updating changes.");
            }
        }
    }
}