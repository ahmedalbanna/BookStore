using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Models // Or your project's namespace
{
    public class Book
    {
        public int BookId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)] // Validate price range
        public decimal Price { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        [MaxLength(255)]
        public string ImageUrl { get; set; }

        [Display(Name = "Upload Image")]
        [NotMapped] // Tell EF Core to ignore this property
        
        public IFormFile ImageFile { get; set; }
    }
}