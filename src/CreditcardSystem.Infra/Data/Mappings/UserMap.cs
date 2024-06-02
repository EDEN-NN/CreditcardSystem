namespace CreditcardSystem.Infra.Data.Mappings;

using CreditcardSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email).IsRequired().HasColumnType("VARCHAR").HasMaxLength(25);

        builder.Property(x => x.Username).IsRequired().HasColumnType("VARCHAR").HasMaxLength(15);

        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.PasswordSalt).IsRequired();
    }
}
