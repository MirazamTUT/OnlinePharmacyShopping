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
            // mvc
            services.AddMvc();

            // Validation
            services.AddScoped<IValidator<CustomerRequestDTO>, CustomerRequestDTOValidator>();
            services.AddScoped<IValidator<CustomerRequestDTOForLogin>, CustomerRequestDTOForLoginValidator>();
            services.AddScoped<IValidator<MedicineRequestDTO>, MedicineRequestDTOValidation>();
            services.AddScoped<IValidator<PaymentRequestDTO>, PaymentRequestDTOValidation>();
            services.AddScoped<IValidator<PharmacyRequestDTO>, PharmacyRequestDTOValidation>();
            services.AddScoped<IValidator<PurchaseRequestDTO>, PurchaseRequestDTOValidation>();
            services.AddScoped<IValidator<ReportRequestDTO>, ReportRequestDTOValidation>();

            // AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            // Repository
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IDataBaseRepository, DataBaseRepository>();
            services.AddScoped<IMedicineRepository, MedicineRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPharmacyRepository, PharmacyRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IReportMedicineRepository, ReportMedicineRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();

            // Services
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IDataBaseService, DataBaseService>();
            services.AddScoped<IMedicineService, MedicineService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPharmacyService, PharmacyService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<ISalesService, SalesService>();
        }
    }
}