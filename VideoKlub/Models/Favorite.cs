using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoKlub.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [Required]
        public int VideoId { get; set; }

        [ForeignKey(nameof(VideoId))]
        public Video Video { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }

    }
}
