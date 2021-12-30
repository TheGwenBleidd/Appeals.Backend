using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appeals.Application.Appeals.Queries.GetAppealDetails
{
    public class GetAppealQuery : IRequest<AppealVm>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}
