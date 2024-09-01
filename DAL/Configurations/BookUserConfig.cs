using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

public class BookUserConfig : IEntityTypeConfiguration<BookUser>
{
    public void Configure(EntityTypeBuilder<BookUser> builder)
    {
        builder.HasIndex(bu => new { bu.BookId, bu.UserId });

        builder.HasKey(x => x.Id);
        
        builder
            .HasOne(b => b.Book)
            .WithMany(bu => bu.BookUsers)
            .HasForeignKey(bi => bi.BookId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
        
        builder
            .HasOne(u => u.User)
            .WithMany(bu => bu.BookUsers)
            .HasForeignKey(ui => ui.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}