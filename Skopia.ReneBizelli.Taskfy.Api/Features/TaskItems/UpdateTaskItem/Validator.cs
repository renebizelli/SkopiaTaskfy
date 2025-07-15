using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.UpdateTaskItem;

public class Validator : AbstractValidator<Request>
{
    private readonly TaskfyDBContext _context;

    public Validator(TaskfyDBContext context)
    {
        _context = context;

        RuleFor(x => x.Title)
        .NotEmpty().WithMessage("INVALID_TITLE");

        RuleFor(x => x.Description)
        .NotEmpty().WithMessage("INVALID_DESCRIPTION");

        RuleFor(x => x.DueAt)
        .GreaterThanOrEqualTo(DateTime.Today).WithMessage("INVALID_DUE_AT");

        RuleFor(x => x.Status)
        .NotEqual(StatusTaskItem.None).WithMessage("INVALID_STATUS");

        RuleFor(x => x.UserResponsibleExternalId)
        .Must(g => string.IsNullOrEmpty(g) || Guid.TryParse(g, out _)).WithMessage("INVALID_USER_EXTERNAL_ID")
        .MustAsync(CheckIfUserExists)
        .When(w => !string.IsNullOrEmpty(w.UserResponsibleExternalId))
        .WithMessage("INVALID_RESPONSIBLE");
    }

    private async Task<bool> CheckIfUserExists(string userExternalId, CancellationToken cancellationToken)
    {
        var externalId = Guid.Parse(userExternalId);

        return await _context.Users.AsNoTracking().AnyAsync(a => a.ExternalId.Equals(externalId), cancellationToken);
    }
}
