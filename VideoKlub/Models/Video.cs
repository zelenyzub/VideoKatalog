using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoKlub.Models
{
    public class Video
    {
        public int Id { get; set; }
        [Required]
        public string Title {  get; set; }

        public string Description { get; set; }

        public string Duration { get; set; }
        public string URL { get; set; }

        public string ImagePath { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        


    }
}
