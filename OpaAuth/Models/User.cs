using System.ComponentModel.DataAnnotations;

namespace OpaAuth.Models
{
    public class User
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [MaxLength(50)]
        [Required]
        public string Role { get; set; }
    }
}
