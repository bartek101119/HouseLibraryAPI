using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseLibraryAPI.Entities
{
    public class HouseLibraryDbContext : DbContext
    {
        private string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=LibraryDb;Trusted_Connection=True;"; // lokalna ścieżka do bazy danych
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author_Book> Author_Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author_Book>()
                .HasOne(a => a.Author)
                .WithMany(ab => ab.Author_Books)
                .HasForeignKey(ai => ai.AuthorId);
            modelBuilder.Entity<Author_Book>()
                .HasOne(b => b.Book)
                .WithMany(ab => ab.Author_Books)
                .HasForeignKey(bi => bi.BookId);

            modelBuilder.Entity<Author>()
                .Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Author>()
                .Property(a => a.LastName)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Author>()
                .Property(a => a.PlaceOfBirth)
                .HasMaxLength(50);
            modelBuilder.Entity<Author>()
                .Property(a => a.Nationality)
                .HasMaxLength(50);

            modelBuilder.Entity<Book>()
               .Property(b => b.Name)
               .IsRequired()
               .HasMaxLength(100);
            modelBuilder.Entity<Book>()
               .Property(b => b.Description)
               .HasMaxLength(3000);
            modelBuilder.Entity<Book>()
               .Property(b => b.Type)
               .HasMaxLength(50);
            modelBuilder.Entity<Book>()
               .Property(b => b.PlaceOfPublication)
               .HasMaxLength(50);
            modelBuilder.Entity<Book>()
               .Property(b => b.Language)
               .HasMaxLength(50);
            modelBuilder.Entity<Book>()
               .Property(b => b.Publisher)
               .HasMaxLength(50);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
