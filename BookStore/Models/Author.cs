using BookStore.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bookstore.Models
{
    public class Author
    {
        public int AuthorId { get; set; }

        [Required]
        [MaxLength(255)]
        public string AuthorName { get; set; }

        [Required]
        public string Biography { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}