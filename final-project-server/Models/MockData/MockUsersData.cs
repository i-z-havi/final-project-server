using final_project_server.Models.Politics;
using final_project_server.Models.Users;

namespace final_project_server.Models.MockData
{
	public class MockUsersData
	{
		public static List<User> GetMockUsers = new List<User>
		{
			new User
			{
				Email = "john.doe@example.com",
				Password = "password123",
				FirstName = "John",
				LastName = "Doe",
				PoliticalLeaning = {PoliticalEnum.Left},
				IsAdmin = false
			},
			new User
			{
				Email = "jane.smith@example.com",
				Password = "securePass456",
				FirstName = "Jane",
				LastName = "Smith",
				PoliticalLeaning = {PoliticalEnum.EconomicRight },
				IsAdmin = true
			},
			new User
			{
				Email = "alex.jones@example.com",
				Password = "freedomNow789",
				FirstName = "Alex",
				LastName = "Jones",
				PoliticalLeaning = {PoliticalEnum.Right },
				IsAdmin = false
			},
			new User
			{
				Email = "amy.johnson@example.com",
				Password = "pass1234",
				FirstName = "Amy",
				LastName = "Johnson",
				PoliticalLeaning = {PoliticalEnum.Left,PoliticalEnum.EconomicLeft},
				IsAdmin = false
			}
		};
	}
}
