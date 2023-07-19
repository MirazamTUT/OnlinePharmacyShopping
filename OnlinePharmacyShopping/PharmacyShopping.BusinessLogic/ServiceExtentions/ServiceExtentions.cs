using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ISalesRepository, SalesRepository>();
            services.AddScoped<IDataBaseRepository, DataBaseRepository>();
            services.AddScoped<IMedicineRepository, MedicineRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IPharmacyService, PharmacyService>();
            services.AddScoped<IDataBaseService, DataBaseService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IMedicineService, MedicineService>();
        }
    }
}
