using FluentValidation;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;

namespace PharmacyShopping.BusinessLogic.DTO.RequestDTOs
{
    public class CustomerRequestDTOForLogin
    {
        public string CustomerFirstName { get; set; }

        public string CustomerLastName { get; set; }

        public string CustomerPassword { get; set; }
    }
}
public class CustomerRequestDTOForLoginValidator : AbstractValidator<CustomerRequestDTOForLogin>
{
    public CustomerRequestDTOForLoginValidator()
    {
        RuleFor(u => u.CustomerFirstName)
            .NotNull().WithMessage("Customer First Name must be entered.")
            .NotEmpty().WithMessage("Customer First Name cannot be empty.")
            .MinimumLength(2).WithMessage("Customer First Name 2 ta belgidan kam bo'lishi mumkin emas.")
            .MaximumLength(15).WithMessage("Customer First Name 15 ta belgidan ko'p bo'lishi mumkin emas.");

        RuleFor(u => u.CustomerLastName)
            .NotNull().WithMessage("Customer Last Name must be entered.")
            .NotEmpty().WithMessage("Customer Last Name cannot be empty")
            .MinimumLength(2).WithMessage("Customer Last Name cannot be less than 2 characters.")
            .MaximumLength(15).WithMessage("Customer Last Name cannot be longer than 15 characters.");

        RuleFor(u => u.CustomerPassword)
            .NotNull().WithMessage("Customer Password must be entered.")
            .NotEmpty().WithMessage("Customer Password cannot be empty.")
            .MinimumLength(8).WithMessage("Customer Password cannot be less than 8 characters.");
    }
}