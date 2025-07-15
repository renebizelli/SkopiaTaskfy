using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;
using Skopia.ReneBizelli.Taskfy.Api.Structure;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.AddCommentary;

public class Validator : AbstractValidator<Request>
{
    private readonly TaskfyDBContext _context;
    private readonly ProjectSettings _settings;

    public Validator(TaskfyDBContext context, IOptions<ProjectSettings> options)
    {
        _context = context;
        _settings = options.Value;

        RuleFor(x => x.Commentary)
        .NotEmpty().WithMessage("INVALID_COMMENTARY");
    }
}
