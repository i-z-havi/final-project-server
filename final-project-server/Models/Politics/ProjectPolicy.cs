using MongoDB.Bson;

namespace final_project_server.Models.Politics
{
	public class ProjectPolicy
	{
		public ObjectId Id { get; set; }
		public string Title { get; set; }
		public string Subtitle { get; set; }
		public string Description { get; set; }
	}
}
