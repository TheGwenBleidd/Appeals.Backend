using Appeals.Application.Common.Exceptions;
using Appeals.Application.Interfaces;
using Appeals.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appeals.Application.Appeals.Commands.UpdateAppeal
{
    public class UpdateAppealCommandHandler
        : IRequestHandler<UpdateAppealCommand>
    {
        private readonly IAppealsDbContext _db;

        public UpdateAppealCommandHandler(IAppealsDbContext db)
        {
            _db = db;
        }

        public async Task<Unit> Handle(UpdateAppealCommand request, CancellationToken cancellationToken)
        {
            var entity =
                await _db.Appeals.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId) 
            {
                throw new NotFoundException(nameof(Appeal), request.Id);
            }

            entity.Title = request.Title;
            entity.Description = request.Description;

            await _db.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
