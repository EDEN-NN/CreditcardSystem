namespace CreditcardSystem.Infra.Data.Mappings;

using CreditcardSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CreditcardMap : IEntityTypeConfiguration<Creditcard>
{
    public void Configure(EntityTypeBuilder<Creditcard> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CardName).IsRequired().HasColumnType("VARCHAR").HasMaxLength(10);

        builder.Property(x => x.CardType).IsRequired().HasColumnType("VARCHAR").HasMaxLength(10);

        builder.Property(x => x.CardBill).IsRequired().HasColumnType("DECIMAL");

        builder
            .HasOne(x => x.Owner)
            .WithMany(x => x.Creditcards)
            .HasForeignKey(x => x.OwnerId)
            .HasConstraintName("FK_Creditcard_Owner")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
