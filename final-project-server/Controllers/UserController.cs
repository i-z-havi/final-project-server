using final_project_server.Models.Users;
using final_project_server.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace final_project_server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private UsersService _usersService;

		public UserController(IMongoClient mongoClient)
		{
			_usersService = new UsersService(mongoClient);
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			List<User> users = await _usersService.GetAllUsersAsync();
			return Ok(users);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get([FromBody] string id)
		{
			User user = await _usersService.GetUserAsync(id);
			return Ok(user);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] User user)
		{
			try
			{
				object DTOuser = await _usersService.CreateUserAsync(user);
				return CreatedAtAction(nameof(Get), new { Id = user.Id }, DTOuser);
			}
			catch(Exception ex) 
			{
				return BadRequest(ex.Message);
			}
		}


	}
}
