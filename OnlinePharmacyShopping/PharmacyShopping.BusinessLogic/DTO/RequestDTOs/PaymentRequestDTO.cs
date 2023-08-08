using FluentValidation;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;

namespace PharmacyShopping.BusinessLogic.DTO.RequestDTOs
{
    public class PaymentRequestDTO
    {
        public int SaleID { get; set; }

        public string CreditCardNumber { get; set; }

        public double TotalPrice { get; set; }

    }
}

public class PaymentRequestDTOValidation : AbstractValidator<PaymentRequestDTO>
{
    public PaymentRequestDTOValidation()
    {
        RuleFor(u => u.CreditCardNumber)
            .NotNull().WithMessage("CreditCard must be entered.")
            .NotEmpty().WithMessage("CreditCard cannot be empty.")
            .Equals(16);

        RuleFor(u => u.TotalPrice)
            .NotNull().WithMessage("TotalPrice must be entered.")
            .NotEmpty().WithMessage("TotalPrice cannot be empty.");
    }
}