using final_project_server.Models.Politics;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace final_project_server.Models.Users
{
	public class UserSQL
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();

		[Required, EmailAddress]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }
		public bool IsAdmin { get; set; } = false;

		public UserSQL() { }

		public UserSQL(UserMongo mongoUser)
		{
			Id = mongoUser.Id.ToString();
			Email = mongoUser.Email;
			Password = mongoUser.Password;
			FirstName = mongoUser.FirstName;
			LastName = mongoUser.LastName;
			IsAdmin = mongoUser.IsAdmin;
		}
	}
}
