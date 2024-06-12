using final_project_server.Models.Politics.Policy_Models;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace final_project_server.Models.Politics
{
    public class ProjectPolicySQL
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; } = false;

        public string? CreatorId { get; set; }

        public ProjectPolicySQL() { }

        public ProjectPolicySQL(ProjectPolicyNormalized policy)
        {
            Id = policy.Id;
            Title = policy.Title;
            Subtitle = policy.Subtitle;
            Description = policy.Description;
            IsActive = policy.IsActive;
            CreatorId = policy.CreatorId;
        }

    }
}
