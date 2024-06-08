using final_project_server.Models.Users.Models;

namespace final_project_server.Services.Data.Repositories.Interfaces
{
    public interface IUserRepository
	{
		public Task<bool> CreateUserAsync(UserNormalized newUser);
		public Task<List<UserSQL>> GetAllUsersAsync(bool includePassword = false); 
		public Task<UserSQL> GetOneUserAsync(string userId, bool includePassword = false);
		public Task<bool> DeleteUserAsync(string userId);
		public Task<UserSQL> EditUserAsync(string userId, UserSQL updatedUser);

		public Task<UserSQL> GetUserByEmail(string email);
	}
}
