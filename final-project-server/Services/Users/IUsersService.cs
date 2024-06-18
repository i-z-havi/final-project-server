using final_project_server.Models.Users;
using final_project_server.Models.Users.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace final_project_server.Services.Users
{
    public interface IUsersService
	{
		Task<object> CreateUserAsync(UserSQL user);

		Task<List<UserSQL>> GetAllUsersAsync();

		Task<UserSQL> GetUserAsync(string userId);

		Task DeleteUserAsync(string userId);

		Task<UserSQL> EditUserAsync(string userId, UserSQL updatedUser);

		Task<UserSQL> LoginAsync(LoginModel loginModel);
	}
}
