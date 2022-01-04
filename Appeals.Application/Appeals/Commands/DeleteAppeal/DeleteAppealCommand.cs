using MediatR;

namespace Appeals.Application.Appeals.Commands.DeleteAppeal
{
    public class DeleteAppealCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}
