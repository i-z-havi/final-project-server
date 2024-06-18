using final_project_server.Models.Politics;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace final_project_server.Models.Users.Models
{
    public class UserSQL
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public bool IsAdmin { get; set; } = false;
        public string? ProfilePicture { get; set; }

        public UserSQL() { }
    }
}
