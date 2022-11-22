using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticeBoardAPI.Models.Validators
{
    public class UpdateAdvertDtoValidator : AbstractValidator<UpdateAdvertDto>
    {
        public UpdateAdvertDtoValidator()
        {
            RuleFor(a => a.Description)
                .NotEmpty()
                .MaximumLength(450);
        }
    }
}
