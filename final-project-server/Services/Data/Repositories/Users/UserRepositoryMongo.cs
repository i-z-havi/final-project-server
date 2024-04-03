using final_project_server.Models.Users;
using final_project_server.Services.Data.Repositories.Interfaces;
using MongoDB.Driver;

namespace final_project_server.Services.Data.Repositories.Users
{
	public class UserRepositoryMongo : IUserRepository
	{
		private IMongoCollection<UserMongo> _users;

		public UserRepositoryMongo(IMongoClient mongoClient)
		{
			//I HAVE NOT MADE THESE! If code doesn't work, make sure these exist in mongodbcompass
			var database = mongoClient.GetDatabase("policy_website");
			_users = database.GetCollection<UserMongo>("users");
		}

		public async Task<bool> CreateUserAsync(UserSQL newUser)
		{
			var check = await _users.Find(u => u.Email == newUser.Email).FirstOrDefaultAsync();
			if (check == null)
			{
				UserMongo userMongo = new UserMongo(newUser);
				await _users.InsertOneAsync(userMongo);
				return true;
			}
			return false;
		}

		public async Task<List<UserSQL>> GetAllUsersAsync(bool includePassword = false)
		{
			var builder = Builders<UserMongo>.Projection;
			var projection = builder.Exclude("Password");
			if (includePassword)
			{
				List<UserMongo> mongoUsers = await _users.Find(_ => true).ToListAsync();
				List<UserSQL> userSQLs = new List<UserSQL>();
				foreach (var user in mongoUsers)
				{
					userSQLs.Add(new UserSQL(user));
				}
				return userSQLs;
			}
			else
			{
				List<UserMongo> mongoUsers = await _users.Find(_ => true).Project<UserMongo>(projection).ToListAsync();
				List<UserSQL> userSQLs = new List<UserSQL>();
				foreach (var user in mongoUsers)
				{
					userSQLs.Add(new UserSQL(user));
				}
				return userSQLs;
			}

		}
		public async Task<UserSQL> GetOneUserAsync(string userId, bool includePassword = false)
		{
			UserMongo specificiedUser = await _users.Find(u => u.Id.ToString() == userId).FirstAsync();
			if (specificiedUser == null)
			{
				return null;
			}
			if (!includePassword)
			{
				specificiedUser.Password = "";
			}
			return new UserSQL(specificiedUser);
		}

		public async Task<bool> DeleteUserAsync(string userId)
		{
			//UserMongo specifiedUser = await _users.Find(u=>u.Id.ToString()==userId).FirstAsync();
			//if (specifiedUser == null)
			//{
			//	return false;
			//}
			//await _users.DeleteOneAsync(u=>u.Id.ToString()== userId);
			//return true;

			var result = await _users.DeleteOneAsync(u => u.Id.ToString() == userId);
			return result.DeletedCount > 0;
		}

		public async Task<UserSQL> EditUserAsync(string userId, UserSQL updatedUser)
		{
			var filter = Builders<UserMongo>.Filter.Eq(u => u.Id.ToString(), userId);

			var update = Builders<UserMongo>.Update
				.Set(u => u.FirstName, updatedUser.FirstName)
				.Set(u => u.LastName, updatedUser.LastName)
				.Set(u => u.Email, updatedUser.Email)
				.Set(u => u.Password, updatedUser.Password)
				.Set(u => u.IsAdmin, updatedUser.IsAdmin);

			var result = await _users.UpdateOneAsync(filter, update);
			if (result.MatchedCount == 0)
			{
				return null;
			}
			return updatedUser;
		}

		public async Task<UserSQL> GetUserByEmail(string email)
		{
			UserMongo user = await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
            if (user==null)
            {
				return null;
            }
			return new UserSQL(user);
        }
	}
}
