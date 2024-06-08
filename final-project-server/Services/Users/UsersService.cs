using final_project_server.Models.Users;
using final_project_server.Models.Users.Models;
using final_project_server.Services.Data.Repositories.Interfaces;
using final_project_server.Utilities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace final_project_server.Services.Users
{
    public class UsersService : IUsersService
	{
		private IUserRepository _users;

		public UsersService(IUserRepository repository)
		{
			_users = repository;
		}

		public async Task<object> CreateUserAsync(UserNormalized user)
		{
			user.Password = PasswordHelper.GeneratePassword(user.Password);

			bool creationSuccessful = await _users.CreateUserAsync(user);

			if (creationSuccessful)
			{
				return new { user.Id, user.Email, user.FirstName };
			}
			else
			{
				throw new Exception("User already exists!");
			}
		}

		public async Task<List<UserSQL>> GetAllUsersAsync()
		{
			List<UserSQL> users = await _users.GetAllUsersAsync();
			return users;
		}

		public async Task<UserSQL> GetUserAsync(string userId)
		{
			UserSQL user = await _users.GetOneUserAsync(userId);
			if (user != null)
			{
				return user;
			}
			else
			{
				throw new Exception("User not found!");
			}
		}

		public async Task DeleteUserAsync(string userId)
		{
			bool isDeleted = await _users.DeleteUserAsync(userId);
			if (!isDeleted)
			{
				throw new Exception("User not found!");
			}
		}

		public async Task<UserSQL> EditUserAsync(string userId, UserSQL updatedUser)
		{
			UserSQL user = await _users.EditUserAsync(userId, updatedUser);
			if (user != null)
			{
				return user;
			}
			else
			{
				throw new Exception("User not found!");
			}
		}

		public async Task<UserSQL> LoginAsync(LoginModel loginModel)
		{
			UserSQL user = await _users.GetUserByEmail(loginModel.UserName);
			if (user != null)
			{
				if (PasswordHelper.VerifyPassword(user.Password, loginModel.Password))
				{
					user.Password = "";
					return user;
				}
				else
				{
					throw new Exception("Incorrect password!");
				}
			}
			else
			{
				throw new Exception("User not found!");
			}
		}
	}
}
