using FluentValidation;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;

namespace PharmacyShopping.BusinessLogic.DTO.RequestDTOs
{
    public class SalesRequestDTO
    {
        public double TotalAmount { get; set; }

        public int PharmacyId { get; set; }

        public int CustomerId { get; set; }

        public int MedicineId { get; set; }

        public int PurchaseId { get; set; }
    }
}

public class SalesRequestDTOValidation : AbstractValidator<SalesRequestDTO>
{
    public SalesRequestDTOValidation()
    {
        RuleFor(u => u.TotalAmount)
            .NotNull().WithMessage("Total Amount ni kiritish kerak.")
            .NotEmpty().WithMessage("total Amount bo'sh bo'lishi mumkin emas.");
    }
}