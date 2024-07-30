using System.Collections.Generic;
using Bookstore.Models;

namespace Bookstore.Data
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAllAuthors();
        Author GetAuthorById(int authorId);
        void AddAuthor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(int authorId);
    }
}