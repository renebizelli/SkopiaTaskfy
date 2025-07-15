using MediatR;
using Microsoft.Data.SqlClient;
using Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;
using Skopia.ReneBizelli.Taskfy._Shared.Services.User;
using Skopia.ReneBizelli.Taskfy.Api.Behaviors.UserRequest;
using Skopia.ReneBizelli.Taskfy.Api.Structure;
using System.Reflection;
using System.Runtime;

var builder = WebApplication.CreateBuilder(args);

var assembly = Assembly.GetEntryAssembly();
ArgumentNullException.ThrowIfNull(assembly);

var services = builder.Services;

services.AddHttpContextAccessor();

services.AddMemoryCache();

services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UserRequestBehavior<,>));
});

services.AddTaskfyDBContext(builder.Configuration);

services.AddUserService();

services.AddEndpoints(assembly);

services.Configure<ProjectSettings>(builder.Configuration.GetSection("Features:Projects"));

var app = builder.Build();


app.MapEndpoints();

app.Run();
