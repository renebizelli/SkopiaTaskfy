using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using System.Data;

namespace Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database.Configurations;

internal class TaskItemHistoryConfiguration : IEntityTypeConfiguration<TaskItemHistory>
{
    public void Configure(EntityTypeBuilder<TaskItemHistory> builder)
    {
        builder.ToTable("TaskItemHistoty");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.TaskItemId).IsRequired();
        builder.Property(x => x.Data).HasColumnType(SqlDbType.Text.ToString()).IsRequired();
        builder.Property(x => x.Type).HasColumnType(SqlDbType.TinyInt.ToString()).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();

        builder.HasOne(m => m.User)
                .WithMany(m => m.History)
                .HasForeignKey(x => x.UserId);

        builder.HasOne(m => m.Task)
                .WithMany(m => m.History)
                .HasForeignKey(x => x.TaskItemId);
    }
}
