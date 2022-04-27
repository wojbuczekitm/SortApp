using FluentValidation;
using SortApp.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortApp.Application.Mediator.Command.Validator
{
    public class SortArrayCommandValidator : AbstractValidator<SortArrayCommand>
    {
        public SortArrayCommandValidator()
        {
            RuleFor(x => x.SortAlgorithm)
                .IsInEnum()
                .WithMessage("Passed value is out of enum range");
        }
    }
}
