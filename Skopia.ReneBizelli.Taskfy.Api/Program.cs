using FluentValidation;
using MediatR;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;
using Skopia.ReneBizelli.Taskfy._Shared.Services.User;
using Skopia.ReneBizelli.Taskfy.Api.Behaviors.UserRequest;
using Skopia.ReneBizelli.Taskfy.Api.Behaviors.Validators;
using Skopia.ReneBizelli.Taskfy.Api.Features.Projects.AddProject;
using Skopia.ReneBizelli.Taskfy.Api.GlobalException;
using Skopia.ReneBizelli.Taskfy.Api.Structure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var assembly = Assembly.GetEntryAssembly();
ArgumentNullException.ThrowIfNull(assembly);

var services = builder.Services;

services.AddHttpContextAccessor();

services.AddMemoryCache();


services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UserRequestBehavior<,>));
});

services.AddValidatorsFromAssembly(assembly);

services.AddExceptionHandler<GlobalExceptionHandler>();
services.AddProblemDetails();

services.AddTaskfyDBContext(builder.Configuration);

services.AddUserService();

services.AddEndpoints(assembly);

services.Configure<ProjectSettings>(builder.Configuration.GetSection("Features:Projects"));

var app = builder.Build();

app.UseExceptionHandler();

app.MapEndpoints();

app.Run();
