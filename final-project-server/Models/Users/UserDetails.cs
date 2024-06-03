using final_project_server.Models.Politics;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace final_project_server.Models.Users
{
    public class UserDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public required string UserId { get; set; }

        public PoliticalEnum Leaning { get; set; }
    }
}
