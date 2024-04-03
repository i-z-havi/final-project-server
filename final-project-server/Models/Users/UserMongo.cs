using final_project_server.Models.Politics;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace final_project_server.Models.Users
{
	public class UserMongo
	{
		public ObjectId Id { get; set; }
		
		[Required,EmailAddress]
		public string Email { get; set; }
		
		[Required]
		public string Password { get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }
		public List<PoliticalEnum> PoliticalLeaning { get; set; }
		public bool IsAdmin { get; set; } = false;

		public UserMongo() { }

		public UserMongo(UserSQL userSQL) 
		{
			Id = new ObjectId(userSQL.Id);
			Email = userSQL.Email;
			Password = userSQL.Password;
			FirstName = userSQL.FirstName;
			LastName = userSQL.LastName;
			IsAdmin = userSQL.IsAdmin;
		}
	}
}
