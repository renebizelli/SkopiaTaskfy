using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using System.Data;

namespace Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database.Configurations;

internal class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable("TaskItems");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.ExternalId).HasColumnType(SqlDbType.UniqueIdentifier.ToString()).IsRequired();
        builder.Property(x => x.Title).HasMaxLength(256).IsRequired();
        builder.Property(x => x.Description).HasColumnType(SqlDbType.Text.ToString()).IsRequired();
        builder.Property(x => x.DueAt).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.Active).IsRequired();
        builder.Property(x => x.Status).HasColumnType(SqlDbType.TinyInt.ToString()).IsRequired();
        builder.Property(x => x.Priority).HasColumnType(SqlDbType.TinyInt.ToString()).IsRequired();

        builder.Property(x => x.UserId).IsRequired(false);

        builder.HasOne(x => x.User)
                .WithMany(o => o.Tasks)
                .HasForeignKey(x => x.UserId);
    }
}
