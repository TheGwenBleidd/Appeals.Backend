using FluentValidation;

namespace Appeals.Application.Appeals.Commands.CreateAppeal
{
    public class CreateAppealCommandValidator : AbstractValidator<CreateAppealCommand>
    {
        public CreateAppealCommandValidator()
        {
            RuleFor(command => command.Title).NotEmpty().MaximumLength(250);
            RuleFor(command => command.UserId).NotEqual(Guid.Empty);
        }
    }
}
