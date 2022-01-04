using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appeals.Application.Appeals.Commands.DeleteAppeal
{
    public  class DeleteAppealCommandValidator : AbstractValidator<DeleteAppealCommand>
    {
        public DeleteAppealCommandValidator()
        {
            RuleFor(command => command.Id).NotEqual(Guid.Empty);
            RuleFor(command => command.UserId).NotEqual(Guid.Empty);
        }
    }
}
