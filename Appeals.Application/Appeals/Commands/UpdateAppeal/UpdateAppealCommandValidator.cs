using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appeals.Application.Appeals.Commands.UpdateAppeal
{
    public class UpdateAppealCommandValidator : AbstractValidator<UpdateAppealCommand>
    {
        public UpdateAppealCommandValidator()
        {
            RuleFor(command => command.Id).NotEqual(Guid.Empty);
            RuleFor(command => command.UserId).NotEqual(Guid.Empty);
            RuleFor(command => command.Title).NotEmpty().MaximumLength(250);
        }
    }
}
