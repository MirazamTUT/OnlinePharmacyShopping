using AutoMapper;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.BusinessLogic.DTO.ResponseDTOs;
using PharmacyShopping.DataAccess.Models;

namespace PharmacyShopping.BusinessLogic.DTO.AutoMapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            // CustomerAutoMapper
            CreateMap<CustomerRequestDTO, Customer>().ReverseMap();
            CreateMap<Customer, CustomerResponseDTO>()
                .ForMember(customerResponseDTO => customerResponseDTO.CustomerFullName,
                opt => opt.MapFrom(customer => $"{customer.CustomerFirstName} {customer.CustomerLastName}"))
                .ReverseMap();

            // DataBaseAutoMapper
            CreateMap<DataBaseRequestDTO, DataBase>().ReverseMap();
            CreateMap<DataBase, DataBaseResponseDTO>().ReverseMap();

            // MedicineAutoMapper
            CreateMap<MedicineRequestDTO, Medicine>().ReverseMap();
            CreateMap<Medicine, MedicineResponseDTO>().ReverseMap();

            // PharmacyAutoMapper
            CreateMap<PharmacyRequestDTO, Pharmacy>().ReverseMap();
            CreateMap<Pharmacy, PharmacyResponseDTO>().ReverseMap();

            // PurchaseAutoMapper
            CreateMap<PurchaseRequestDTO, Purchase>().ReverseMap();
            CreateMap<Purchase, PurchaseResponseDTO>()
                .ForMember(purchaseResponseDTO => purchaseResponseDTO.CustomerFullName,
                opt => opt.MapFrom(purchase => $"{purchase.Customer.CustomerFirstName} {purchase.Customer.CustomerLastName}"))
                .ForMember(purchaseResponseDTO => purchaseResponseDTO.MedicineName,
                opt => opt.MapFrom(purchase => purchase.Medicine.MedicineName))
                .ReverseMap();

            // ReportAutoMapper
            CreateMap<ReportRequestDTO, Report>().ReverseMap();
            CreateMap<Report, ReportResponseDTO>()
                .ForMember(reportResponseDTO => reportResponseDTO.CustomerFullName,
                opt => opt.MapFrom(report => $"{report.Customer.CustomerFirstName} {report.Customer.CustomerLastName}"))
                .ReverseMap();
            
            // SalesAutoMapper
            CreateMap<SalesRequestDTO, Sale>().ReverseMap();
            CreateMap<Sale, SalesResponseDTO>()
                .ForMember(salesResponseDTO => salesResponseDTO.PharmacyName,
                opt => opt.MapFrom(sales => sales.Pharmacy.PharmacyName))
                .ForMember(salesResponseDTO => salesResponseDTO.CustomerFullName,
                opt => opt.MapFrom(sales => $"{sales.Customer.CustomerFirstName} {sales.Customer.CustomerLastName}"))
                .ReverseMap();
        }
    }
}