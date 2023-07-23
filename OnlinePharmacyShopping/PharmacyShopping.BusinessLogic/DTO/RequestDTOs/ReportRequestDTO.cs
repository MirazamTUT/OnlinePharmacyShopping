using FluentValidation;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;

namespace PharmacyShopping.BusinessLogic.DTO.RequestDTOs
{
    public class ReportRequestDTO
    {
        public string ReportDescription { get; set; }

        public int PurchaseId { get; set; }

        public int CustomerId { get; set; }

        public List<int> MedicineId { get; set; }
    }
}

public class ReportRequestDTOValidation : AbstractValidator<ReportRequestDTO>
{
    public ReportRequestDTOValidation()
    {
        RuleFor(u => u.ReportDescription)
            .NotNull().WithMessage("Report must be entered.")
            .NotEmpty().WithMessage("Report cannot be empty.");
    }
}