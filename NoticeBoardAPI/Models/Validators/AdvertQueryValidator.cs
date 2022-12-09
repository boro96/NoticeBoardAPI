using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticeBoardAPI.Models.Validators
{
    public class AdvertQueryValidator : AbstractValidator<AdvertQuery>
    {
        private int[] allowedPageSizes = new[] { 5, 10, 15 };
        public AdvertQueryValidator()
        {
            RuleFor(a => a.pageNumber).GreaterThanOrEqualTo(1);
            RuleFor(b => b.pageSize).Custom((value, context) =>
            {
                if(!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("pageSize", $"PageSize must be in [{string.Join(",", allowedPageSizes)}]");
                }
            });
        }
    }
}
