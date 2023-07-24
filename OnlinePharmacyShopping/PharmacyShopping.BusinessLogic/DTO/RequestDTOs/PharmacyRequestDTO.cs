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
            .NotNull().WithMessage("Pharmacy Name must be entered.")
            .NotEmpty().WithMessage("Pharmacy Name cannot be empty.")
            .MinimumLength(2).WithMessage("Pharmacy Name cannot be less than 2 characters.")
            .MaximumLength(15).WithMessage("Pharmacy Name cannot be longer than 15 characters.");

        RuleFor(u => u.PhoneNumber)
            .NotNull().WithMessage("Phone Number must be entered.")
            .NotEmpty().WithMessage("Phone Number cannot be empty.");

        RuleFor(u => u.Email)
            .NotNull().WithMessage("Email must be entered.")
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Entered Email incorrectly.");

        RuleFor(u => u.Password)
            .NotNull().WithMessage("Password must be entered.")
            .NotEmpty().WithMessage("Password cannot be empty.")
            .MinimumLength(8).WithMessage("Password cannot be less than 8 characters");
    }
}
