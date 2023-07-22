using FluentValidation;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;

namespace PharmacyShopping.BusinessLogic.DTO.RequestDTOs
{
    public class PurchaseRequestDTO
    {
        public double Amount { get; set; }

        public int CustomerId { get; set; }

        public int MedicineId { get; set; }

        public DateTime PurchaseDate { get; set; }
    }
}

public class PurchaseRequestDTOValidation : AbstractValidator<PurchaseRequestDTO>
{
    public PurchaseRequestDTOValidation()
    {
        RuleFor(u => u.Amount)
            .NotNull().WithMessage("Amount kiritish kerak.")
            .NotEmpty().WithMessage("Amount bo'sh bo'lmasligi kerak.");

        RuleFor(u => u.PurchaseDate)
            .NotNull().WithMessage("Purchase date ni kiritish kerak.")
            .NotEmpty().WithMessage("Purchase date bo'sh bo'lishi mumkin emas.")
            .LessThanOrEqualTo(DateTime.Today).WithMessage("Purchase date bugun yoki bugundan avval bo'lishi kerak.");           
    }
}