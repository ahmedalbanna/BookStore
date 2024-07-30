using Microsoft.AspNetCore.Mvc;
using Bookstore.Data;
using Bookstore.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Http;

namespace Bookstore.Controllers
{
    public class BookController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository; // Inject the AuthorRepository

        public BookController(IBookRepository bookRepository, IAuthorRepository authorRepository, IHostingEnvironment hostingEnvironment)
        {
            
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: /Book/
        public IActionResult Index()
        {
            var books = _bookRepository.GetAllBooks();
            return View(books); // Pass the list of books to the view
        }

        // GET: /Book/Details/5
        public IActionResult Details(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound(); // Handle case where the book doesn't exist
            }
            return View(book); // Pass the book details to the view
        }

        // GET: /Book/Create
        public IActionResult Create()
        {
            ViewBag.Authors = _authorRepository.GetAllAuthors();
            var book = new Book { ImageUrl = "/images/books/default.png" }; // Default image
            return View(); // Show an empty form to create a new book
        }

        // POST: /Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book, IFormFile imageFile)
        {
            if (!ModelState.IsValid) // Validate form data
            {
                // Save the image to a folder (e.g., wwwroot/images/books)
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "books");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                // Set the ImageUrl property for the Book
                book.ImageUrl = "/images/books/" + uniqueFileName;
                _bookRepository.AddBook(book); // Add the book to the database
                return RedirectToAction("Index"); // Redirect to the book list
            }
            return View(book); // If validation fails, redisplay the form with errors
        }

        // GET: /Book/Edit/5
        public IActionResult Edit(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            // Set a default ImageUrl if one doesn't exist
            if (string.IsNullOrEmpty(book.ImageUrl))
            {
                book.ImageUrl = "/images/books/default.png";
            }
            // Handle potential null values 
            var authors = _authorRepository.GetAllAuthors();
            if (authors == null)
            {
                // Handle the case where authors are not found (e.g., display a message)
                return View(book); // Or return an error page
            }
            ViewBag.Authors = authors;

            //ViewBag.Authors = _authorRepository.GetAllAuthors();
            ViewBag.ImageUrl = book.ImageUrl;
            return View(book); // Pass the book details to the edit view
        }

        // POST: /Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                
                // Check if ImageUrl has changed
                var originalImageUrl = book.ImageUrl;

                // Handle image upload
                //if (book.ImageFile != null || book.ImageUrl != originalImageUrl)
                if (book.ImageUrl == null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/books");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + book.ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Save the image to the uploads folder
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await book.ImageFile.CopyToAsync(fileStream);
                    }

                    // Update the ImageUrl property in your Book object
                    book.ImageUrl = "/images/books/" + uniqueFileName;
                }

                // If the book doesn't have an ImageUrl yet, set a default
                if (string.IsNullOrEmpty(book.ImageUrl))
                {
                    book.ImageUrl = "/images/books/default.png";
                }
                _bookRepository.UpdateBook(book); // Update the book in the database
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: /Book/Delete/5
        public IActionResult Delete(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: /Book/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _bookRepository.DeleteBook(id);
            return RedirectToAction("Index");
        }
    }
}