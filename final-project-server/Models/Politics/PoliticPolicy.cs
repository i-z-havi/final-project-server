using MongoDB.Bson;

namespace final_project_server.Models.Politics
{
    public class PoliticPolicy
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public List<PoliticalEnum> PoliticalLean { get; set; }
    }
}
