using System.ComponentModel.DataAnnotations;

namespace final_project_server.Models.Politics
{
    public class ProjectPolicySign
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public required string PolicyId {  get; set; }

        public required string UserId { get; set; }
    }
}
