using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

public class AuthorBookConf : IEntityTypeConfiguration<AuthorBook>
{
    public void Configure(EntityTypeBuilder<AuthorBook> builder)
    {
        builder.HasIndex(ind => new { ind.AuthorId, ind.BookId });

        builder.HasIndex(pk => pk.Id);

        builder
            .HasOne(x => x.Book)
            .WithMany(x => x.AuthorBooks)
            .HasForeignKey(x => x.BookId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasOne(x => x.Author)
            .WithMany(x => x.AuthorBooks)
            .HasForeignKey(x => x.AuthorId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}