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
		
		//create
		public async Task<object> CreateUserAsync(User user)
		{
            if (_users.Find(u=>u.Email==user.Email).FirstOrDefaultAsync()!=null)
            {
				throw new Exception("User with this email already exists");
            }
			//Hash password here BEFORE insert!
            await _users.InsertOneAsync(user);
			return new {user.FirstName, user.Email, user.Id};
		}
		
		//get all
		public async Task<List<User>> GetAllUsersAsync()
		{
			return await _users.Find(_=>true).ToListAsync();
		}

		//get one 
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

		//delete
		public async Task DeleteUserAsync(string userId)
		{
			var result = await _users.DeleteOneAsync(u=>u.Id.ToString()==userId);
            if (result.DeletedCount==0)
            {
				throw new Exception("User not found!");
            }
        }

		// edit/update
		public async Task EditUserAsync(string userId, User updatedUser)
		{
			var filter = Builders<User>.Filter.Eq(u=>u.Id, new ObjectId(userId));
			var update = Builders<User>.Update
				.Set(u => u.FirstName, updatedUser.FirstName)
				.Set(u => u.LastName, updatedUser.LastName)
				.Set(u => u.Email, updatedUser.Email)
				.Set(u => u.IsAdmin, updatedUser.IsAdmin)
				.Set(u => u.Password, updatedUser.Password)
				.Set(u => u.PoliticalLeaning, updatedUser.PoliticalLeaning);

			var result = _users.UpdateOne(filter, update);

            if (result.MatchedCount==0)
            {
                throw new Exception("User not found!");
            }
        }
	}
}
