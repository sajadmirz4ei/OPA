using System.ComponentModel.DataAnnotations;

namespace OpaAuth.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
