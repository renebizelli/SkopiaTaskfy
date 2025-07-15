using MediatR;
using Skopia.ReneBizelli.Taskfy.Api.Structure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var assembly = Assembly.GetEntryAssembly();
ArgumentNullException.ThrowIfNull(assembly);

var services = builder.Services;

services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
});

services.AddEndpoints(assembly);

var app = builder.Build();

app.MapEndpoints();

app.Run();
