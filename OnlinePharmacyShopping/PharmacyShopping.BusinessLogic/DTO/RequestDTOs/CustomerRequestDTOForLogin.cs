using FluentValidation;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;

namespace PharmacyShopping.BusinessLogic.DTO.RequestDTOs
{
    public class CustomerRequestDTOForLogin
    {
        public string CustomerFullName { get; set; }

        public string CustomerPassword { get; set; }
    }
}
public class CustomerRequestDTOForLoginValidator : AbstractValidator<CustomerRequestDTOForLogin>
{
    public CustomerRequestDTOForLoginValidator()
    {
        RuleFor(u => u.CustomerFullName)
            .NotNull().WithMessage("Customer Full Name must be entered.")
            .NotEmpty().WithMessage("Customer Full Name cannot be empty.")
            .MinimumLength(2).WithMessage("Customer Full Name 2 ta belgidan kam bo'lishi mumkin emas.")
            .MaximumLength(50).WithMessage("Customer Full Name 50 ta belgidan ko'p bo'lishi mumkin emas.");

        RuleFor(u => u.CustomerPassword)
            .NotNull().WithMessage("Customer Password must be entered.")
            .NotEmpty().WithMessage("Customer Password cannot be empty.")
            .MinimumLength(8).WithMessage("Customer Password cannot be less than 8 characters.");
    }
}