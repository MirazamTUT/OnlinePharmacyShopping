using FluentValidation;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;

namespace PharmacyShopping.BusinessLogic.DTO.RequestDTOs
{
    public class PurchaseRequestDTO
    {
        public double Amount { get; set; }

        public int CustomerId { get; set; }

        public int SaleId { get; set; }

        public int MedicineId { get; set; }

        public DateTime PurchaseDate { get; set; }
    }
}

public class PurchaseRequestDTOValidation : AbstractValidator<PurchaseRequestDTO>
{
    public PurchaseRequestDTOValidation()
    {
        RuleFor(u => u.Amount)
            .NotNull().WithMessage("Amount must be entered.")
            .NotEmpty().WithMessage("Amount cannot be empty.");

        RuleFor(u => u.PurchaseDate)
            .NotNull().WithMessage("Purchase date must be entered.")
            .NotEmpty().WithMessage("Purchase date cannot be empty.")
            .LessThanOrEqualTo(DateTime.Today).WithMessage("Purchase date must be today or before today.");
    }
}