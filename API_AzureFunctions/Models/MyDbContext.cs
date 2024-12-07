using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API_AzureFunctions.Models;

public partial class MyDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public MyDbContext(IConfiguration configuration, DbContextOptions<MyDbContext> options)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Availability> Availabilities { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Stream> Streams { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("LibraryManagement");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Availability>(entity =>
        {
            entity.ToTable("Availability");

            entity.Property(e => e.Quantity).IsUnicode(false);

            entity.HasOne(d => d.Book).WithMany(p => p.Availabilities)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Availability_BOOK");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("BOOK");

            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.Title).IsUnicode(false);

            entity.HasOne(d => d.Stream).WithMany(p => p.Books)
                .HasForeignKey(d => d.StreamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BOOK_STREAM");
        });

        modelBuilder.Entity<Stream>(entity =>
        {
            entity.ToTable("STREAM");

            entity.Property(e => e.Name).IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
