using Microsoft.AspNetCore.Mvc;
using Bookstore.Data;
using Bookstore.Models;

namespace Bookstore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        // GET: /Author/
        public IActionResult Index()
        {
            var authors = _authorRepository.GetAllAuthors();
            return View(authors);
        }


        // GET: /Author/Details/5
        public IActionResult Details(int id)
        {
            var author = _authorRepository.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }
            // Ensure the books are included in the Author object
            author = _authorRepository.GetAuthorById(id);
            return View(author); //// Pass the Author object to the Details view
        }

        // GET: /Author/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Author/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {
                _authorRepository.AddAuthor(author);
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // GET: /Author/Edit/5
        public IActionResult Edit(int id)
        {
            var author = _authorRepository.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: /Author/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Author author)
        {
            if (id != author.AuthorId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _authorRepository.UpdateAuthor(author);
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // GET: /Author/Delete/5
        public IActionResult Delete(int id)
        {
            var author = _authorRepository.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: /Author/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _authorRepository.DeleteAuthor(id);
            return RedirectToAction("Index");
        }
    }
}