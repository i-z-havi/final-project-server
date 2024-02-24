using final_project_server.Models.Users;
using MongoDB.Bson;
using MongoDB.Driver;

namespace final_project_server.Services.Users
{
	public interface IUsersService
	{
		Task<object> CreateUserAsync(User user);

		Task<List<User>> GetAllUsersAsync();

		Task<User> GetUserAsync(string userId);

		Task DeleteUserAsync(string userId);

		Task EditUserAsync(string userId, User updatedUser);
	}
}
