using MongoDB.Bson;

namespace final_project_server.Models.Politics
{
	public class ProjectPolicyMongo
	{
		public ObjectId Id { get; set; }

		public string Title { get; set; }

		public string Subtitle { get; set; }

		public string Description { get; set; }

        public List<PolicyDetails> Details { get; set; }

        public List<ProjectPolicySign> Signatures { get; set; }
    }
}
