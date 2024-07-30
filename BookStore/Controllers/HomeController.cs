using Bookstore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        // private readonly ILogger<HomeController> _logger; // Commented out
        
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        // Constructor with IBookRepository injected
        public HomeController(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public IActionResult Index()
        {
            // Get books from the repository
            var books = _bookRepository.GetAllBooks();
            return View(books); // Pass the books to the view // Render the Home/Index view
        }

        // Commented out:
        /*public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /*// GET: /Home/Search?searchTerm=
        public IActionResult Search(string searchTerm)
        {
            // Search for books
            var books = _bookRepository.GetAllBooks()
                                      .Where(b =>
                                          b.Title.Contains(searchTerm) ||
                                          b.Genre.Contains(searchTerm) ||
                                          b.Author.AuthorName.Contains(searchTerm)); // No need for ToList() here

            // Search for authors
            var authors = _authorRepository.GetAllAuthors()
                                        .Where(a =>
                                            a.AuthorName.Contains(searchTerm));

            // Combine results
            var searchResults = new { Books = books.ToList(), Authors = authors.ToList() }; // Use ToList() for the view

            return View(searchResults);
        }*/

        // Optional: Add actions for other pages (About, Contact, etc.)

    }
}