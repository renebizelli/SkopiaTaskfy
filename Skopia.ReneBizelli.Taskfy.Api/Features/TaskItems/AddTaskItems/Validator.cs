using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;
using Skopia.ReneBizelli.Taskfy.Api.Structure;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.AddTaskItems;

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
        .Must(g => Guid.TryParse(g, out _)).WithMessage("INVALID_PROJECT_EXTERNAL_ID")
        .MustAsync(CheckIfProjectExists).WithMessage("INVALID_RESPONSIBLE");

        RuleFor(x => x.UserResponsibleExternalId)
        .Must(g => string.IsNullOrEmpty(g) || Guid.TryParse(g, out _)).WithMessage("INVALID_PROJECT_EXTERNAL_ID")
        .MustAsync(CheckIfUserExists)
        .When(w => !string.IsNullOrEmpty(w.UserResponsibleExternalId))
        .WithMessage("INVALID_RESPONSIBLE");

        RuleFor(r => r).CustomAsync(async (o, context, cancellationToken) =>
        {
            var externalId = Guid.Parse(o.ProjectExternalId);

            var project = await _context.Projects.AsNoTracking().FirstOrDefaultAsync(a => a.ExternalId.Equals(externalId), cancellationToken);

            var count = _context.TaskItems.Where(w => w.Active).Count(f => f.ProjectId.Equals(project!.Id));

            if(count == project!.TaskItemsLimit)
            {
                context.AddFailure("EXCEEDED_NUMBER_OF_TASKS");
            }
        });
    }

    private async Task<bool> CheckIfUserExists(string userExternalId, CancellationToken cancellationToken)
    {
        var externalId = Guid.Parse(userExternalId);

        return await _context.Users.AsNoTracking().AnyAsync(a => a.ExternalId.Equals(externalId), cancellationToken);
    }

    private async Task<bool> CheckIfProjectExists(string projectExternalId, CancellationToken cancellationToken)
    {
        var externalId = Guid.Parse(projectExternalId);

        return await _context.Projects.AsNoTracking().AnyAsync(a => a.ExternalId.Equals(externalId), cancellationToken);
    }
}
