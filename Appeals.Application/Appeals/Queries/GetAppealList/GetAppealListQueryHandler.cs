using Appeals.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appeals.Application.Appeals.Queries.GetAppealList
{
    public class GetAppealListQueryHandler
        : IRequestHandler<GetAppealListQuery, AppealListVm>
    {
        private readonly IAppealsDbContext _db;
        private readonly IMapper _mapper;
        public GetAppealListQueryHandler(IAppealsDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<AppealListVm> Handle(GetAppealListQuery request, CancellationToken cancellationToken)
        {
            var notesQuery = await _db.Appeals
                .Where(x => x.UserId == request.UserId)
                .ProjectTo<AppealLookUpDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
                ;

            return new AppealListVm { Appeals = notesQuery };
        }
    }
}
