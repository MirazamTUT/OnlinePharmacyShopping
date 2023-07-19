using PharmacyShopping.DataAccess.Models;

namespace PharmacyShopping.DataAccess.Repository.IRepositories
{
    public interface IReportMedicineRepository
    {
        Task AddReportMedicineAsync(ReportMedicine reportMedicine);

        Task UpdateReportMedicineAsync(ReportMedicine reportMedicine);

        Task DeleteReportMedicineAsync(ReportMedicine reportMedicine);

        Task<List<ReportMedicine>> GetAllReportMedicineByMedicineIdAsync(int id);

        Task<List<ReportMedicine>> GetAllReportMedicine();

        Task<List<ReportMedicine>> GetAllReportMedicineByReportId(int id);
    }
}
