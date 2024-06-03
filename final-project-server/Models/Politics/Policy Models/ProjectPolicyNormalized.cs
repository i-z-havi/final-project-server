namespace final_project_server.Models.Politics.Policy_Models
{
    public class ProjectPolicyNormalized
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; } = false;

        public string CreatorId {  get; set; }

        public List<PoliticalEnum>? Details { get; set; }

        public List<string>? Signatures { get; set; }

        //default
        public ProjectPolicyNormalized() { }

        //from SQL
        public ProjectPolicyNormalized(ProjectPolicySQL sqlPolicy, List<PoliticalEnum> details, List<string> signatureList)
        {
            Id = sqlPolicy.Id;
            Title = sqlPolicy.Title;
            Subtitle = sqlPolicy.Subtitle;
            Description = sqlPolicy.Description;
            IsActive = sqlPolicy.IsActive;
            CreatorId = sqlPolicy.CreatorId;
            Details = details;
            Signatures = signatureList;
        }
    }
}
