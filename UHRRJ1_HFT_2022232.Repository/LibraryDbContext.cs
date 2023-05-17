using Microsoft.EntityFrameworkCore;
using UHRRJ1_HFT_2022232.Models;

namespace UHRRJ1_HFT_2022232.Repository
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public DbSet<Library> Libraries { get; set; }

        public DbSet<Reader> Readers { get; set; }

        public DbSet<Author> Authors { get; set; }

        public LibraryDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseLazyLoadingProxies().UseInMemoryDatabase("db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(Book => Book
                .HasOne(Book => Book.Author)
                .WithMany(Author => Author.Books)
                .HasForeignKey(Book => Book.AuthorId)
                .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Reader>()
                .HasMany(x => x.Books)
                .WithMany(x => x.Readers)
                .UsingEntity<Library>(
                    x => x.HasOne(x => x.Book)
                        .WithMany().HasForeignKey(x => x.BookId).OnDelete(DeleteBehavior.Cascade),
                    x => x.HasOne(x => x.Reader)
                        .WithMany().HasForeignKey(x => x.ReaderId).OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Library>()
                .HasOne(r => r.Reader)
                .WithMany(Reader => Reader.Libraries)
                .HasForeignKey(r => r.ReaderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Library>()
                .HasOne(r => r.Book)
                .WithMany(Book => Book.Libraries)
                .HasForeignKey(r => r.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            //Seed
        }
    }
}
