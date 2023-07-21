using PharmacyShopping.DataAccess.Models;

namespace PharmacyShopping.DataAccess.Repository.IRepositories
{
    public interface IReportMedicineRepository
    {
        Task AddReportMedicineAsync(ReportMedicine reportMedicine);

        Task UpdateReportMedicineAsync(ReportMedicine reportMedicine);

        Task DeleteReportMedicineAsync(ReportMedicine reportMedicine);

        Task<List<ReportMedicine>> GetAllReportMedicineByMedicineIdAsync(int id);

        Task<List<ReportMedicine>> GetAllReportMedicineAsync();

        Task<List<ReportMedicine>> GetAllReportMedicineByReportIdAsync(int id);
    }
}
