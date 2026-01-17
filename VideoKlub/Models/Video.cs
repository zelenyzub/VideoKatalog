using System.ComponentModel.DataAnnotations;

namespace VideoKlub.Models
{
    public class Video
    {
        public int Id { get; set; }
        [Required]
        public string Title {  get; set; }

        public string Description { get; set; }

        //trebace kategorija naknadno

        public string Duration { get; set; }
        public string URL { get; set; }


    }
}
