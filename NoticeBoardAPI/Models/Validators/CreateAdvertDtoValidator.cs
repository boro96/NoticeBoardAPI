using FluentValidation;
using NoticeBoardAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticeBoardAPI.Models.Validators
{
    public class CreateAdvertDtoValidator : AbstractValidator<CreateAdvertDto>
    {
        public CreateAdvertDtoValidator(NoticeBoardDbContext context)
        {
            RuleFor(a => a.Description)
                .NotEmpty()
                .MaximumLength(450);

            RuleFor(b => b.CategoryName)
                .Custom((value, context) =>
                {
                    
                });
        }
    }
}
