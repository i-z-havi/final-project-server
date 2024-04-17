using final_project_server.Models.Politics;
using System.ComponentModel.DataAnnotations;

namespace final_project_server.Models.Users
{
    public class UserNormalized
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

        public List<PoliticalEnum> PoliticalLeanings { get; set; }

        public bool IsAdmin { get; set; } = false;

    }
}
