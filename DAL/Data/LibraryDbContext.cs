using DAL.Configurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options){}
   

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AuthorBookConf());
    }
}