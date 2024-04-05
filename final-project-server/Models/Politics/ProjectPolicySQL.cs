using MongoDB.Bson;

namespace final_project_server.Models.Politics
{
	public class ProjectPolicySQL
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();
		public string Title { get; set; }
		public string Subtitle { get; set; }
		public string Description { get; set; }
		public bool IsActive { get; set; } = false;
	}
}
