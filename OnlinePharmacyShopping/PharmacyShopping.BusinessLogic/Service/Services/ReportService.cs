using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;
using PharmacyShopping.BusinessLogic.Service.IServices;
using PharmacyShopping.DataAccess.Repository.IRepositories;
using PharmacyShopping.DataAccess.Models;

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
                throw new Exception("Connection between database is failed");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> DeleteReportAsync(int Id)
        {
            try
            {
                var reportResult = await _reportRepository.GetReportByIdAsync(Id);
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
                throw new Exception("Operation was failed when it was giving the info");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ReportResponseDTO> GetReportByIdAsync(int Id)
        {
            try
            {
                return _mapper.Map<ReportResponseDTO>(await _reportRepository.GetReportByIdAsync(Id));
            }
            catch (AutoMapperMappingException ex)
            {
                throw new Exception("Mapping failed");
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation was failed when it was giving the info");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> UpdateReportAsync(ReportRequestDTO reportRequestDTO, int Id)
        {
            try
            {
                var reportResult = await _reportRepository.GetReportByIdAsync(Id);
                if (reportResult is not null)
                {
                    reportResult = _mapper.Map<Report>(reportRequestDTO);
                    reportResult.ReportId = Id;
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
            catch (DbUpdateException ex)
            {
                throw new Exception("Connection between database is failed");
            }
            catch (Exception ex)
            {
                throw new Exception("Operation was failed when it updating changes");
            }
        }
    }
}
