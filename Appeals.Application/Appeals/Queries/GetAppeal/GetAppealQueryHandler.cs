using Appeals.Application.Common.Exceptions;
using Appeals.Application.Interfaces;
using Appeals.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appeals.Application.Appeals.Queries.GetAppeal
{
    internal class GetAppealQueryHandler
        : IRequestHandler<GetAppealQuery, AppealVm>
    {
        private readonly IAppealsDbContext _db;
        private readonly IMapper _mapper;

        public GetAppealQueryHandler(IAppealsDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<AppealVm> Handle(GetAppealQuery request, CancellationToken cancellationToken)
        {
            var entity = await _db.Appeals
                    .FirstOrDefaultAsync(x => x.Id == request.Id,cancellationToken);

            if (entity == null || entity.UserId != request.UserId) 
                throw new NotFoundException(nameof(Appeal), request.Id);

            return _mapper.Map<AppealVm>(entity);
        }
    }
}
