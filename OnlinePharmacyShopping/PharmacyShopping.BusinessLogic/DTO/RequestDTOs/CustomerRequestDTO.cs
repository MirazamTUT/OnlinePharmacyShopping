using FluentValidation;
using Microsoft.AspNetCore.Http;
using PharmacyShopping.BusinessLogic.DTO.RequestDTOs;
using PharmacyShopping.DataAccess.Models;

namespace PharmacyShopping.BusinessLogic.DTO.RequestDTOs
{
    public class CustomerRequestDTO
    {
        public string CustomerFirstName { get; set; }

        public string CustomerLastName { get; set; }

        public Gender Gender { get; set; }

        public IFormFile formFile { get; set; }

        public DateOnly BirthDate { get; set; }

        public string PhoneNumber { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerPassword { get; set; }
    }
}

public class CustomerRequestDTOValidator : AbstractValidator<CustomerRequestDTO>
{
    public CustomerRequestDTOValidator()
    {
        RuleFor(u => u.CustomerFirstName)
            .NotNull().WithMessage("Customer First Name must be entered.")
            .NotEmpty().WithMessage("Customer First Name cannot be empty.")
            .MinimumLength(2).WithMessage("Customer First Name 2 ta belgidan kam bo'lishi mumkin emas.")
            .MaximumLength(15).WithMessage("Customer First Name 15 ta belgidan ko'p bo'lishi mumkin emas.");

        RuleFor(u => u.CustomerLastName)
            .NotNull().WithMessage("Customer Last Name must be entered.")
            .NotEmpty().WithMessage("Customer Last Name cannot be empty")
            .MinimumLength(2).WithMessage("Customer Last Name cannot be less than 2 characters.")
            .MaximumLength(15).WithMessage("Customer Last Name cannot be longer than 15 characters.");

       // RuleFor(u => u.BirthDate)
          // .NotNull().WithMessage("Birth date must be entered.")
           // .LessThan(DateOnly.MinValue).WithMessage("Birth date cannot be earliest");

        RuleFor(u => u.Gender)
            .NotNull().WithMessage("Gender must be entered.")
            .IsInEnum().WithMessage("Entered incorrectly.");

        RuleFor(u => u.PhoneNumber)
            .NotNull().WithMessage("Phone Number must be entered.")
            .NotEmpty().WithMessage("Phone Number cannot be empty.");

        RuleFor(u => u.CustomerEmail)
            .NotNull().WithMessage("Customer Email must be entered.")
            .NotEmpty().WithMessage("Customer Email cannot be empty.")
            .EmailAddress().WithMessage("Entered Email incorrectly.");

        RuleFor(u => u.CustomerPassword)
            .NotNull().WithMessage("Customer Password must be entered.")
            .NotEmpty().WithMessage("Customer Password cannot be empty.")
            .MinimumLength(8).WithMessage("Customer Password cannot be less than 8 characters.");
    }
}