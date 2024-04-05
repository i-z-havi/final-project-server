using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace final_project_server.Models.Politics
{
    public class PolicyDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public required string PolicyId { get; set; }

        public PoliticalEnum Leaning { get; set; }
    }
}
