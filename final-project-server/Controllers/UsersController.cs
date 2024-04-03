using final_project_server.Models.Users;
using final_project_server.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace final_project_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersService _usersService;

        public UsersController(IUsersService service)
        {
            _usersService = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<UserSQL> users = await _usersService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                UserSQL user = await _usersService.GetUserAsync(id);
                return Ok(user);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }



        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserSQL user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                object DTOuser = await _usersService.CreateUserAsync(user);
                return CreatedAtAction(nameof(Get), new { Id = user.Id }, DTOuser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] UserSQL updatedUser)
        {
            try
            {
                await _usersService.EditUserAsync(id, updatedUser);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _usersService.DeleteUserAsync(id);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return NoContent();
        }
    }
}
