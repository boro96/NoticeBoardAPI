using FluentValidation;
using NoticeBoardAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticeBoardAPI.Models.Validators
{
    public class RegisterUsedDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUsedDtoValidator(NoticeBoardDbContext dbContext)
        {
            RuleFor(a => a.Email)
                .NotEmpty()
                .EmailAddress()
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(a => a.Email == value);
                    if(emailInUse)
                    {
                        context.AddFailure("Email", "Email is taken");
                    }
                });
            RuleFor(a => a.Password)
                .NotEmpty()
                .MinimumLength(8);

            RuleFor(a => a.ConfirmPassword)
                .Equal(b => b.Password);

            RuleFor(a => a.FirstName)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(a => a.LastName)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(a => a.Nationality)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(a => a.City)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(b => b.Street)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(a => a.ApartamentNumber)
                .NotEmpty()
                .MaximumLength(10);

            RuleFor(a => a.PostalCode)
                .NotEmpty()
                .MaximumLength(15);


                


        }
    }
}
