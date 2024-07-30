using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using Bookstore.Models;

namespace BookStore.Models // Assuming your models are in the 'BookStore.Models' namespace
{
    public class BookstoreDbContext : DbContext // Inherit from DbContext
    {
        public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}