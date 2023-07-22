using FluentValidation;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;

namespace PharmacyShopping.BusinessLogic.DTO.RequestDTOs
{
    public class PharmacyRequestDTO
    {
        public string PharmacyName { get; set; }

        public int DataBaseId { get; set; }

        public List<string> PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}

public class PharmacyRequestDTOValidation : AbstractValidator<PharmacyRequestDTO>
{
    public PharmacyRequestDTOValidation()
    {
        RuleFor(u => u.PharmacyName)
            .NotNull().WithMessage("Pharmacy Name kiritish kerak.")
            .NotEmpty().WithMessage("Pharmacy Name bo'sh bo'lishi mumkin emas.")
            .MinimumLength(2).WithMessage("Pharmacy Name 2 ta belgidan kam bo'lmasligi kerak.")
            .MaximumLength(15).WithMessage("Pharmacy Name 15 ta belgidan ko'p bo'lmasligi kerak.");

        RuleFor(u => u.PhoneNumber)
            .NotNull().WithMessage("Phone Number kiritish kerak.")
            .NotEmpty().WithMessage("Phone Number bo'sh bo'lishi mumkin emas.");
          //.Matches(@"^[0-9]{10}$").WithErrorCode("Phone Number noto'g'ri kiritilgan.");

        RuleFor(u => u.Email)
            .NotNull().WithMessage("Email kiritish kerak.")
            .NotEmpty().WithMessage("Email bo'sh bo'lishi mumkin emas.")
            .EmailAddress().WithMessage("Noto'g'ri Email manzil kiritilgan");

        RuleFor(u => u.Password)
            .NotNull().WithMessage("Password kiritish kerak.")
            .NotEmpty().WithMessage("Password bo'sh bo'lishi mumkin emas.")
            .MinimumLength(8).WithMessage("Password 8 ta belgidan kam bo'lmasligi kerak.");
    }
}