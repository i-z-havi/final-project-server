﻿using final_project_server.Authentication;
using final_project_server.Models.Users;
using final_project_server.Models.Users.Models;
using final_project_server.Services;
using final_project_server.Services.Users;
using final_project_server.Uploads;
using final_project_server.Utilities;
using Microsoft.AspNetCore.Authorization;
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
        private readonly KeyVaultService _keyVaultService;

        public UsersController(IUsersService service, KeyVaultService keyVaultService)
        {
            _usersService = service;
            _keyVaultService = keyVaultService;
        }

        [HttpGet]
        [Authorize(Policy = "isAdmin")]
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
            catch (Exception ex)
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
            updatedUser.Password = PasswordHelper.GeneratePassword(updatedUser.Password);
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                UserSQL? userCheck = await _usersService.LoginAsync(model);
                string jwtKey = await _keyVaultService.GetSecretAsync("JwtKey");
                string token = JwtHelper.GenerateAuthToken(userCheck, jwtKey);
                return Ok(token);
            }
            catch
            {
                return Unauthorized("Incorrect Email or Password!");
            }
        }
    }
}
