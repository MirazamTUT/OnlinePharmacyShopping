using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.Service.IServices;
using PharmacyShopping.BusinessLogic.Service.Services;
using PharmacyShopping.DataAccess.Repository.IRepositories;
using PharmacyShopping.DataAccess.Repository.Repositories;

namespace PharmacyShopping.BusinessLogic.ServiceExtentions
{
    public static class ServiceExtentions
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc();
            services.AddScoped<IValidator<CustomerRequestDTO>, CustomerRequestDTOValidator>();
            services.AddScoped<IValidator<MedicineRequestDTO>, MedicineRequestDTOValidation>();
            services.AddScoped<IValidator<PharmacyRequestDTO>, PharmacyRequestDTOValidation>();
            services.AddScoped<IValidator<PurchaseRequestDTO>, PurchaseRequestDTOValidation>();
            services.AddScoped<IValidator<ReportRequestDTO>, ReportRequestDTOValidation>();
            services.AddScoped<IValidator<SaleRequestDTO>, SaleRequestDTOValidation>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<IDataBaseRepository, DataBaseRepository>();
            services.AddScoped<IMedicineRepository, MedicineRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IPharmacyRepository, PharmacyRepository>();
            services.AddScoped<IReportMedicineRepository, ReportMedicineRepository>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IPharmacyService, PharmacyService>();
            services.AddScoped<IDataBaseService, DataBaseService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IMedicineService, MedicineService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<ISalesService, SalesService>();
        }
    }
}