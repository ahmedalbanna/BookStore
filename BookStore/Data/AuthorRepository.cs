using System.Collections.Generic;
using Bookstore.Models;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Data
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookstoreDbContext _context;

        public AuthorRepository(BookstoreDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _context.Authors.Include(a => a.Books).ToList(); // Include Books for related data
        }

        public Author GetAuthorById(int authorId)
        {
            // return _context.Authors.Include(a => a.Books).FirstOrDefault(a => a.AuthorId == authorId); // Include Books
            //return _context.Authors.FirstOrDefault(a => a.AuthorId == authorId);
            return _context.Authors
        .Include(a => a.Books) // Eagerly load the Books collection
        .FirstOrDefault(a => a.AuthorId == authorId);
        }

        public void AddAuthor(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public void UpdateAuthor(Author author)
        {
            _context.Entry(author).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteAuthor(int authorId)
        {
            var author = _context.Authors.Find(authorId);
            if (author != null)
            {
                _context.Authors.Remove(author);
                _context.SaveChanges();
            }
        }
        public IEnumerable<Author> SearchAuthors(string searchTerm)
        {
            return _context.Authors.Where(a => a.AuthorName.ToLower().Contains(searchTerm.ToLower())).ToList();
        }
    }
}