using FluentValidation;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;

namespace PharmacyShopping.BusinessLogic.DTO.RequestDTOs
{
    public class PurchaseRequestDTO
    {
        public int Amount { get; set; }

        public int CustomerId { get; set; }

        public int SaleId { get; set; }

        public int MedicineId { get; set; }
    }
}

public class PurchaseRequestDTOValidation : AbstractValidator<PurchaseRequestDTO>
{
    public PurchaseRequestDTOValidation()
    {
        RuleFor(u => u.Amount)
            .NotNull().WithMessage("Amount must be entered.")
            .NotEmpty().WithMessage("Amount cannot be empty.")
            .GreaterThan(0).WithMessage("Amount must be greater than 0.");
    }
}