using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VideoKlub.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Video> Videos { get; set; }
    }
}
