using final_project_server.Models.Users;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace final_project_server.Services.Users
{
	public class UsersService
	{
		private IMongoCollection<User> _users;
		
		public UsersService(IMongoClient mongoClient) 
		{
			var database = mongoClient.GetDatabase("policies_website");
			_users = database.GetCollection<User>("users");
		}
		
		public async Task<User> CreateUserAsync(User user)
		{
			await _users.InsertOneAsync(user);
			return user;
		}
		
		public async Task<List<User>> GetAllUsersAsync()
		{
			return await _users.Find(_=>true).ToListAsync();
		}

		public async Task<User> GetUserAsync(string userId)
		{
			var builder = Builders<User>.Projection;
			var projection = builder.Exclude("Password");
			User user = await _users.Find(u=>u.Id.ToString() == userId)  
				.Project<User>(projection)
				.FirstOrDefaultAsync();
			if (user == null)
			{
				throw new Exception("User not found");
			}
			return user;
		}
	}
}
