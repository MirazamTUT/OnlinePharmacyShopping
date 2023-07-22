using FluentValidation;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.DataAccess.Models;

namespace PharmacyShopping.BusinessLogic.DTO.RequestDTOs
{
    public class CustomerRequestDTO
    {
        public string CustomerFirstName { get; set; }

        public string CustomerLastName { get; set; }

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public string PhoneNumber { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerPassword { get; set; }
    }
}

public class CustomerRequestDTOValidator : AbstractValidator<CustomerRequestDTO>
{
    public CustomerRequestDTOValidator()
    {
        RuleFor(u => u.CustomerFirstName)
            .NotNull().WithMessage("Customer First Name ni kiritish kerak.")
            .NotEmpty().WithMessage("Customer First Name bo'sh bo'lishi mumkin emas.")
            .MinimumLength(2).WithMessage("Customer First Name 2 ta belgidan kam bo'lishi mumkin emas.")
            .MaximumLength(15).WithMessage("Customer First Name 15 ta belgidan ko'p bo'lishi mumkin emas.");

        RuleFor(u => u.CustomerLastName)
            .NotNull().WithMessage("Customer Last Name ni kiritish kerak.")
            .NotEmpty().WithMessage("Customer Last Name bo'sh bo'lishi mumkin emas.")
            .MinimumLength(2).WithMessage("Customer Last Name 2 ta belgidan kam bo'lishi mumkin emas.")
            .MaximumLength(15).WithMessage("Customer Last Name 15 ta belgidan ko'p bo'lishi mumkin emas.");

        RuleFor(u => u.BirthDate)
            .NotNull().WithMessage("Birth date ni kiritish kerak.")
            .LessThan(DateTime.Today).WithMessage("Birth date sanasi o'tmishda bo'lishi kerak.");

        RuleFor(u => u.Gender)
            .NotNull().WithMessage("Gender ni kiritish kerak.")
            .IsInEnum().WithMessage("Noto'g'ri kiritilgan.");

        RuleFor(u => u.PhoneNumber)
            .NotNull().WithMessage("Phone Number ni kiritish kerak.")
            .NotEmpty().WithMessage("Phone Number bo'sh bo'la olmaydi.")
            .Matches(@"^[0-9]{10}$").WithErrorCode("Phone Number noto'g'ri kiritilgan.");

        RuleFor(u => u.CustomerEmail)
            .NotNull().WithMessage("Customer Email kiritish kerak.")
            .NotEmpty().WithMessage("Customer Email bo'sh bo'lishi mumkin emas.")
            .EmailAddress().WithMessage("Noto'g'ri Email manzil kiritilgan.");

        RuleFor(u => u.CustomerPassword)
            .NotNull().WithMessage("Customer Password ni kiritish kerak.")
            .NotEmpty().WithMessage("Customer Password bo'sh bo'lishi mumkin emas.")
            .MinimumLength(8).WithMessage("Passwordni uzunligi 8 ta belgidan kam bolishi mumkin emas.");
    }
}