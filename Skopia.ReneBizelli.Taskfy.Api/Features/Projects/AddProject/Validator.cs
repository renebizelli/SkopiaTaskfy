using FluentValidation;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.Projects.AddProject;

public  class Validator : AbstractValidator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Name)
       .NotEmpty().WithMessage("INVALID_NAME");
    }
}
