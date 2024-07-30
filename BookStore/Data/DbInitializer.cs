using Bookstore.Models;
using BookStore.Models;

namespace Bookstore.Data
{
    public class DbInitializer
    {
        private readonly BookstoreDbContext _context;

        public DbInitializer(BookstoreDbContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            // Check if the database is empty
            if (_context.Books.Any() || _context.Authors.Any())
            {
                return; // Database already has data
            }

            // Seed authors
            var authors = new Author[]
            {
                new Author { AuthorName = "Stephen King", Biography = "Prolific horror author" },
                new Author { AuthorName = "J.K. Rowling", Biography = "Fantasy author known for Harry Potter" },
                new Author { AuthorName = "Jane Austen", Biography = "Classical author" },
            };

            foreach (var author in authors)
            {
                _context.Authors.Add(author);
            }
            _context.SaveChanges();

            // Seed books
            var books = new Book[]
            {
                new Book { Title = "It", Genre = "Horror", Price = 15, AuthorId = authors[0].AuthorId, ImageUrl = "/img/book1.png" },
                new Book { Title = "Harry Potter and the Sorcerer's Stone", Genre = "Fantasy", Price = 12, AuthorId = authors[1].AuthorId, ImageUrl = "/img/book2.png" },
                new Book { Title = "Pride and Prejudice", Genre = "Romance", Price = 9, AuthorId = authors[2].AuthorId, ImageUrl = "/img/book3.png" }
            };

            foreach (var book in books)
            {
                _context.Books.Add(book);
            }
            _context.SaveChanges();
        }
    }
}