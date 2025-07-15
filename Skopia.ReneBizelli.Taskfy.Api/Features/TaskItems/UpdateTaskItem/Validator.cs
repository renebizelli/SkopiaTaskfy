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

        RuleFor(x => x.UserExternalId)
        .NotEqual(Guid.Empty).WithMessage("INVALID_USER_EXTERNAL_ID")
        .MustAsync(CheckIfUserExists)
        .When(w => !w.UserExternalId.Equals(Guid.Empty))
        .WithMessage("INVALID_RESPONSIBLE");
    }

    private async Task<bool> CheckIfUserExists(Guid userExternalId, CancellationToken cancellationToken)
        => await _context.Users.AsNoTracking().AnyAsync(a => a.ExternalId.Equals(userExternalId), cancellationToken);

}
