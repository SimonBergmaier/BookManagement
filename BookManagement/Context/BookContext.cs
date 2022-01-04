using BookManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Context
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().ToTable(nameof(Author));
            modelBuilder.Entity<Book>().ToTable(nameof(Book));
            modelBuilder.Entity<Genre>().ToTable(nameof(Genre));
        }

    }
}
