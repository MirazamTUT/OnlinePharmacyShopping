using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;
using PharmacyShopping.BusinessLogic.Service.IServices;
using PharmacyShopping.DataAccess.Models;
using PharmacyShopping.DataAccess.Repository.IRepositories;

namespace PharmacyShopping.BusinessLogic.Service.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IReportMedicineRepository _reportMedicineRepository;
        private readonly IMapper _mapper;

        public ReportService(IReportRepository reportRepository, IMapper mapper, IReportMedicineRepository reportMedicineRepository)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
            _reportMedicineRepository = reportMedicineRepository;
        }
        
        public async Task<int> AddReportAsync(ReportRequestDTO reportRequestDTO)
        {
            try
            {
                var resultReportId = await _reportRepository.AddReportAsync(_mapper.Map<Report>(reportRequestDTO));
                foreach (int medicineId in reportRequestDTO.MedicineId)
                {
                    var reportMedicine = new ReportMedicine
                    {
                        ReportId = resultReportId,
                        MedicineId = medicineId
                    };
                    await _reportMedicineRepository.AddReportMedicineAsync(reportMedicine);
                }
                return resultReportId;
            }
            catch (AutoMapperMappingException ex)
            {
                throw new Exception("Mapping failed");
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was adding changes");
            }
        }

        public async Task<int> DeleteReportAsync(int id)
        {
            try
            {
                var reportResult = await _reportRepository.GetReportByIdAsync(id);
                if (reportResult is not null)
                {
                    await _reportRepository.DeleteReportAsync(reportResult);
                    foreach (ReportMedicine reportMedicine in reportResult.ReportMedicines)
                    {
                        await _reportMedicineRepository.DeleteReportMedicineAsync(reportMedicine);
                    }
                    return reportResult.ReportId;
                }
                else
                {
                    throw new Exception("Object cannot be deleted");
                }
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Connection between database is failed");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was deleting changes");
            }
        }

        public async Task<List<ReportResponseDTO>> GetAllReportsAsync()
        {
            try
            {
                return _mapper.Map<List<ReportResponseDTO>>(await _reportRepository.GetAllReportsAsync());
            }
            catch (AutoMapperMappingException ex)
            {
                throw new Exception("Mapping failed");
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ReportResponseDTO> GetReportByIdAsync(int id)
        {
            try
            {
                return _mapper.Map<ReportResponseDTO>(await _reportRepository.GetReportByIdAsync(id));
            }
            catch (AutoMapperMappingException ex)
            {
                throw new Exception("Mapping failed");
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdateReportAsync(ReportRequestDTO reportRequestDTO, int id)
        {
            try
            {
                var reportResult = await _reportRepository.GetReportByIdAsync(id);
                if (reportResult is not null)
                {
                    reportResult = _mapper.Map<Report>(reportRequestDTO);
                    reportResult.ReportId = id;
                    var resultReportId = await _reportRepository.UpdateReportAsync(reportResult);
                    foreach (int medicineId in reportRequestDTO.MedicineId)
                    {
                        var resultReportMedicine = await _reportMedicineRepository.GetAllReportMedicineByMedicineIdAsync(medicineId);
                        for (int i = 0; i < resultReportMedicine.Count(); i++)
                        {
                            resultReportMedicine[i].ReportId = reportResult.MedicineId[i];
                            var reportMedicineForUpdate = resultReportMedicine[i];
                            await _reportMedicineRepository.UpdateReportMedicineAsync(reportMedicineForUpdate);
                        }
                    }
                    return resultReportId;
                }
                else
                {
                    throw new Exception("Object cannot be updated");
                }
            }
            catch (AutoMapperMappingException ex)
            {
                throw new Exception("Mapping failed");
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Connection between database is failed");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it was updating changes");
            }
        }
    }
}
