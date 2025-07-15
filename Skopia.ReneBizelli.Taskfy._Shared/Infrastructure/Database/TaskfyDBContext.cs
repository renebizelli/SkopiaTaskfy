using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database;

public class TaskfyDBContext : DbContext
{

    public TaskfyDBContext(DbContextOptions<TaskfyDBContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskfyDBContext).Assembly); 

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Entities.Project> Projects { get; set; }
    public DbSet<Entities.User> Users { get; set; }
    public DbSet<Entities.TaskItem> TaskItems { get; set; }
    public DbSet<Entities.TaskItemHistory> TaskItemHistories { get; set; }

}

public static class TaskfyDBContextConfiguration
{
    public static void AddTaskfyDBContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TaskfyDBContext>( options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("sqlTaskfy"));
        });
    }
}

public class TaskfyDBContextFactory : IDesignTimeDbContextFactory<TaskfyDBContext>
{
    public TaskfyDBContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true) // Use AddJsonFile directly
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<TaskfyDBContext>();
        var connectionString = configuration.GetConnectionString("sqlTaskfy");

        optionsBuilder.UseSqlServer(connectionString);

        return new TaskfyDBContext(optionsBuilder.Options);
    }
}

