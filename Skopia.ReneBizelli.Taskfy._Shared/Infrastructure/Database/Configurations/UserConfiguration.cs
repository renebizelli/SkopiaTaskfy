using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Skopia.ReneBizelli.Taskfy._Shared.Entities;
using System.Data;

namespace Skopia.ReneBizelli.Taskfy._Shared.Infrastructure.Database.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.ExternalId).HasColumnType(SqlDbType.UniqueIdentifier.ToString()).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(128).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(256).IsRequired();
        builder.Property(x => x.Role).HasColumnType(SqlDbType.TinyInt.ToString()).IsRequired();
    }
}
