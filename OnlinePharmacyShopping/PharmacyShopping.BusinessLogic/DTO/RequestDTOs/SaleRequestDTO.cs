using FluentValidation;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;

namespace PharmacyShopping.BusinessLogic.DTO.RequestDTOs
{
    public class SaleRequestDTO
    {
        public double TotalAmount { get; set; }

        public int PharmacyId { get; set; }

        public int CustomerId { get; set; }
    }
}

public class SaleRequestDTOValidation : AbstractValidator<SaleRequestDTO>
{
    public SaleRequestDTOValidation()
    {
        RuleFor(u => u.TotalAmount)
            .NotNull().WithMessage("Total Amount must be entered.")
            .NotEmpty().WithMessage("total Amount cannot be empty.");
    }
}