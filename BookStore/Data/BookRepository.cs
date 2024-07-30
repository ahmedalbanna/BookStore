using System.Collections.Generic;
using Bookstore.Models;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly BookstoreDbContext _context;

        public BookRepository(BookstoreDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books.Include(b => b.Author).ToList(); // Include Author for related data
        }

        public Book GetBookById(int bookId)
        {
            return _context.Books.Include(b => b.Author).FirstOrDefault(b => b.BookId == bookId); // Include Author
        }

        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteBook(int bookId)
        {
            var book = _context.Books.Find(bookId);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
    }
}