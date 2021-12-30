using Appeals.Application.Interfaces;
using Appeals.Domain;
using MediatR;

namespace Appeals.Application.Appeals.Commands.CreateAppeal
{
    public class CreateAppealCommandHandler
        : IRequestHandler<CreateAppealCommand, Guid>
    {
        private readonly IAppealsDbContext _db;

        public CreateAppealCommandHandler(IAppealsDbContext db)
        {
            _db = db;
        }

        public async Task<Guid> Handle(CreateAppealCommand request, CancellationToken cancellationToken)
        {
            var appeal = new Appeal()
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
                Id = Guid.NewGuid(),
                CreatedDate = DateTimeOffset.Now,
            };

            await _db.Appeals.AddAsync(appeal, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            return appeal.Id;
        }
    }
}
