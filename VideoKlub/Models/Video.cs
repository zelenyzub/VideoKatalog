using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace VideoKlub.Models
{
    public class Video
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Naslov je obavezan!")]
        public required string Title {  get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Vreme trajanja je obavezno!")]
        public required string Duration { get; set; }
        public string? URL { get; set; }

        public string? ImagePath { get; set; }

        [Required(ErrorMessage = "Morate izabrati kategoriju.")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }

        public bool IsActive { get; set; } = true;
        [NotMapped]
        public bool IsFavorite { get; set; } = false;

        [NotMapped]
        [Required(ErrorMessage = "Morate izabrati sliku za video.")]
        public IFormFile? ImageFile { get; set; }
    }
}
