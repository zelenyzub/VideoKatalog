using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoKlub.Models
{
    public class Rate
    {
        public int Id { get; set; }

        [Range(1, 5)]
        [Required]
        public int Value { get; set; }

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // FK ka Video
        [Required]
        public int VideoId { get; set; }

        [ForeignKey(nameof(VideoId))]
        public Video Video { get; set; }

        // FK ka IdentityUser
        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
    }
}
