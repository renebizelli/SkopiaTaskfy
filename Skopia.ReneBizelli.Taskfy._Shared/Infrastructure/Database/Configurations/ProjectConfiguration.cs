using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using System.Data;

namespace Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database.Configurations;

internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.ExternalId).HasColumnType(SqlDbType.UniqueIdentifier.ToString()).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(64).IsRequired();
        builder.Property(x => x.TaskItemsLimit).IsRequired();
        builder.Property(x => x.Active).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();

        builder.HasMany(m => m.Tasks)
                .WithOne(o => o.Project)
                .HasForeignKey(k => k.ProjectId);

        builder.HasOne(o => o.Author)
            .WithMany(m => m.AuthoredProjects)
            .HasForeignKey(f => f.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Users)
            .WithMany(u => u.Projects)
            .UsingEntity("ProjectsUsers");
    }
}
