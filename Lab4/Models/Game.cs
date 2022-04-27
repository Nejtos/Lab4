using System.ComponentModel.DataAnnotations;

namespace Lab4.Models
{
    public class Game
    {
        public int ID { get; set; }

        public string Title { get; set; } = string.Empty;

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime RelaseDate { get; set; }
        public string Genre { get; set; } = string.Empty;
        public string? Platforms { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Range(1, 100)]
        public string Rating { get; set; } = string.Empty;

        [Display(Name = "Game Image")]
        public string? GameImage { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
