using System.Collections.Generic;
using Bookstore.Models;

namespace Bookstore.Data
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(int bookId);
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int bookId);
    }
}