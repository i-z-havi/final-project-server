using System.ComponentModel.DataAnnotations;

namespace final_project_server.Models.Politics
{
    public class PolicyDetail
    {
        public string Id = Guid.NewGuid().ToString();

        public required string PolicyId { get; set; }

        public PoliticalEnum Leaning { get; set; }
    }
}
