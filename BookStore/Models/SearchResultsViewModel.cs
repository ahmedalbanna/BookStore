using System.Collections.Generic;
using Bookstore.Models;

namespace Bookstore.Models
{
    public class SearchResultsViewModel
    {
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Author> Authors { get; set; } = new List<Author>();
    }
}