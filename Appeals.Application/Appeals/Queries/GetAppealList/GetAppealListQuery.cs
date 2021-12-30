using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appeals.Application.Appeals.Queries.GetAppealList
{
    public class GetAppealListQuery : IRequest<AppealListVm>
    {
        public Guid UserId { get; set; }
    }
}
