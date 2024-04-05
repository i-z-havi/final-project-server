using final_project_server.Models.Users;
using final_project_server.Services.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace final_project_server.Services.Data.Repositories.Users
{
    public class UserRepositoryEF : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepositoryEF(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUserAsync(UserSQL user)
        {
            var newUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (newUser == null)
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<UserSQL>> GetAllUsersAsync(bool includePassword = false)
        {
            List<UserSQL> users = await _context.Users.ToListAsync();
            if (!includePassword)
            {
                foreach (var user in users)
                {
                    user.Password = "";
                }
            }
            return users;
        }

        public async Task<UserSQL> GetOneUserAsync(string userId, bool includePassword = false)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return null;
            }
            if (!includePassword)
            {
                user.Password = "";
            }
            return user;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<UserSQL> EditUserAsync(string userId, UserSQL updatedUser)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return null;
            }
            user.Password = updatedUser.Password;
            user.Email = updatedUser.Email;
            user.IsAdmin = updatedUser.IsAdmin;
            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            await _context.SaveChangesAsync();
            user.Password = " ";
            return user;
        }

        public async Task<UserSQL> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}
