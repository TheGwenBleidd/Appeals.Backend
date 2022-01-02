using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appeals.Application.Appeals.Queries.GetAppeal
{
    internal class GetAppealQueryValidator : AbstractValidator<GetAppealQuery>
    {
        public GetAppealQueryValidator()
        {
            RuleFor(query => query.Id).NotEqual(Guid.Empty);
            RuleFor(query => query.UserId).NotEqual(Guid.Empty);
        }
    }
}
