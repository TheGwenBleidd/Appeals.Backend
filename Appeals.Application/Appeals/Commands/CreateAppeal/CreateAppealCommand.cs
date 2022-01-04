using MediatR;

namespace Appeals.Application.Appeals.Commands.CreateAppeal
{
    public class CreateAppealCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
