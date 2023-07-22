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
            .NotNull().WithMessage("Medicine Name ni kiritish kerak.")
            .NotEmpty().WithMessage("Medicne Name bo'sh bo'lishi mumkin emas.")
            .MinimumLength(2).WithMessage("Medicine Name 2 ta belgidan kam bolmasligi kerak.")
            .MaximumLength(20).WithMessage("Medicine Name 20 ta belgidan ko'p bo'lmasligi kerak.");

        RuleFor(u => u.MedicineCategory)
            .NotNull().WithMessage("Medicine Category ni kiritish kerak.")
            .NotEmpty().WithMessage("Medicine Category bo'sh bo'lishi mumkin emas.");

        RuleFor(u => u.MedicineDescription)
            .NotNull().WithMessage("Medicine Description ni kiritish kerak.")
            .NotEmpty().WithMessage("Medicine Description bo'sh bo'lishi mumkin emas.");

        RuleFor(u => u.MedicinePrice)
            .NotNull().WithMessage("Medicine Price ni kiritish kerak.")
            .NotEmpty().WithMessage("Medicine Price bo'sh bo'lishi mumkin emas.");

        RuleFor(u => u.AmountOfMedecine)
            .NotNull().WithMessage("Amount of Medicine ni kiritish kerak.")
            .NotEmpty().WithMessage("Amount of Medicine bo' bo'lishi mumkin emas.");
    }
}