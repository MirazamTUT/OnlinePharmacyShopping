using FluentValidation;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;

namespace PharmacyShopping.BusinessLogic.DTO.RequestDTOs
{
    public class MedicineRequestDTO
    {
        public int DataBaseId { get; set; }

        public string MedicineName { get; set; }

        public string MedicineCategory { get; set; }

        public string MedicineDescription { get; set; }

        public double MedicinePrice { get; set; }

        public double AmountOfMedecine { get; set; }
    }
}

public class MedicineRequestDTOValidation : AbstractValidator<MedicineRequestDTO>
{
    public MedicineRequestDTOValidation()
    {
        RuleFor(u => u.MedicineName)
            .NotNull().WithMessage("Medicine Name must be entered.")
            .NotEmpty().WithMessage("Medicne Name cannot be empty.")
            .MinimumLength(2).WithMessage("Medicine Name cannot be less than 2 characters.")
            .MaximumLength(20).WithMessage("Medicine Name cannot be longer than 20 characters.");

        RuleFor(u => u.MedicineCategory)
            .NotNull().WithMessage("Medicine Category must be entered.")
            .NotEmpty().WithMessage("Medicine Category cannot be empty.");

        RuleFor(u => u.MedicineDescription)
            .NotNull().WithMessage("Medicine Description must be entered.")
            .NotEmpty().WithMessage("Medicine Description cannot be empty.");

        RuleFor(u => u.MedicinePrice)
            .NotNull().WithMessage("Medicine Price must be entered.")
            .NotEmpty().WithMessage("Medicine Price cannot be empty.");

        RuleFor(u => u.AmountOfMedecine)
            .NotNull().WithMessage("Amount of Medicine must be entered.")
            .NotEmpty().WithMessage("Amount of Medicine cannot be empty.");
    }
}