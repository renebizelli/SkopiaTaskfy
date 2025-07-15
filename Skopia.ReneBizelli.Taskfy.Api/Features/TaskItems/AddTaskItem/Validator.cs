using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;
using Skopia.ReneBizelli.Taskfy.Api.Structure;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.AddTaskItem;

public class Validator : AbstractValidator<Request>
{
    private readonly TaskfyDBContext _context;
    private readonly ProjectSettings _settings;

    public Validator(TaskfyDBContext context, IOptions<ProjectSettings> options)
    {
        _context = context;
        _settings = options.Value;

        RuleFor(x => x.Title)
        .NotEmpty().WithMessage("INVALID_TITLE");

        RuleFor(x => x.Description)
        .NotEmpty().WithMessage("INVALID_DESCRIPTION");

        RuleFor(x => x.DueAt)
        .GreaterThanOrEqualTo(DateTime.Today).WithMessage("INVALID_DUE_AT");

        RuleFor(x => x.Status)
        .NotEqual(StatusTaskItem.None).WithMessage("INVALID_STATUS");

        RuleFor(x => x.Priority)
        .NotEqual(PriorityTaskItem.None).WithMessage("INVALID_PRIORITY");

        RuleFor(x => x.ProjectExternalId)
        .NotEqual(Guid.Empty).WithMessage("INVALID_PROJECT_EXTERNAL_ID")
        .MustAsync(CheckIfProjectExists).WithMessage("INVALID_RESPONSIBLE");

        RuleFor(x => x.UserExternalId)
        .NotEqual(Guid.Empty).WithMessage("INVALID_USER_EXTERNAL_ID")
        .MustAsync(CheckIfUserExists)
        .When(w => !w.UserExternalId.Equals(Guid.Empty))
        .WithMessage("INVALID_RESPONSIBLE");


    }

    private async Task<bool> CheckIfUserExists(Guid userExternalId, CancellationToken cancellationToken)
        => await _context.Users.AsNoTracking().AnyAsync(a => a.ExternalId.Equals(userExternalId), cancellationToken);

    private async Task<bool> CheckIfProjectExists(Guid projectExternalId, CancellationToken cancellationToken)
        => await _context.Projects.AsNoTracking().AnyAsync(a => a.ExternalId.Equals(projectExternalId), cancellationToken);
}
