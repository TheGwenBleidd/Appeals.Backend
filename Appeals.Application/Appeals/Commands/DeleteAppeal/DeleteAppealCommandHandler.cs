using Appeals.Application.Common.Exceptions;
using Appeals.Application.Interfaces;
using Appeals.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appeals.Application.Appeals.Commands.DeleteAppeal
{
    public class DeleteAppealCommandHandler
        : IRequestHandler<DeleteAppealCommand>
    {
        private readonly IAppealsDbContext _db;

        public DeleteAppealCommandHandler(IAppealsDbContext db)
        {
            _db = db;
        }

        public async Task<Unit> Handle(DeleteAppealCommand request, CancellationToken cancellationToken)
        {
            var entity = await _db.Appeals.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Appeal), request.Id);
            }

            _db.Appeals.Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}
