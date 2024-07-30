using Microsoft.AspNetCore.Mvc;
using Bookstore.Data;
using Bookstore.Models;
using System.Linq;
using System.Text.RegularExpressions;

namespace Bookstore.Controllers
{
    public class SearchController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public SearchController(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        // GET: /Search/Results?searchTerm=...
        public IActionResult Results(string searchTerm)
        {
            // Handle null or whitespace search terms
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                // Display an error message in the view
                ViewData["ErrorMessage"] = "Please enter a search term.";
                return View(); // Render the Search/Results view with the error message
            }


            // Search for books
            var books = _bookRepository.GetAllBooks()
                .Where(b => b.Title.ToLower().Contains(searchTerm.ToLower()) ||
                            b.Genre.ToLower().Contains(searchTerm.ToLower()) ||
                            b.Author.AuthorName.ToLower().Contains(searchTerm.ToLower()))
                .ToList();

            // Search for authors
            var authors = _authorRepository.GetAllAuthors()
                .Where(a => a.AuthorName.ToLower().Contains(searchTerm.ToLower()) ||
                            a.Books.Any(b => b.Genre.ToLower().Contains(searchTerm.ToLower())) ||
                            a.Books.Any(b => b.Title.ToLower().Contains(searchTerm.ToLower())))
                .ToList();

            // Combine results
            var searchResults = new SearchResultsViewModel
            {
                Books = books,
                Authors = authors
            };

            // ... other logic 
            ViewData["searchTerm"] = searchTerm;

            return View(searchResults);
        }
    }
}