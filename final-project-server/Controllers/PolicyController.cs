using final_project_server.Models.Politics;
using final_project_server.Models.Politics.Policy_Models;
using final_project_server.Services.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Security.Claims;

namespace final_project_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private IPoliciesService _policiesService;

        public PolicyController(IPoliciesService policiesService)
        {
            _policiesService = policiesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPolicies()
        {
            List<ProjectPolicyNormalized> policies = await _policiesService.GetPoliciesAsync();
            return Ok(policies);
        }

        [HttpGet("pending")]
        [Authorize(Policy = "isAdmin")]
        public async Task<IActionResult> GetPendingPolicies()
        {
            List<ProjectPolicyNormalized> pendingPolicies = await _policiesService.GetPendingPoliciesAsync();
            return Ok(pendingPolicies);
        }

        [HttpGet("my_petitions")]
        public async Task<IActionResult> GetMyPoliciesAsync()
        {
            string id = HttpContext.User.FindFirstValue("id") ?? "";
            Console.WriteLine(id);
            List<ProjectPolicyNormalized> myPolicies = await _policiesService.GetMyPoliciesAsync(id);
            return Ok(myPolicies);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPolicy(string id)
        {
            ProjectPolicyNormalized pol = await _policiesService.GetPolicyAsync(id);
            if (pol != null)
            {
                return Ok(pol);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePolicy([FromBody] ProjectPolicyNormalized policy)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ProjectPolicyNormalized pol = await _policiesService.CreatePolicyAsync(policy);
            return CreatedAtAction(nameof(GetPolicy), new { id = pol.Id }, pol);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePolicy(string id, [FromBody] ProjectPolicyNormalized pol)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _policiesService.UpdatePolicyAsync(id, pol);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> SignPolicy(string id, string userid)
        {
            //MAKE THIS USE HTTPCONTEXT USER! this was only to check if the request goes through
            try
            {
                await _policiesService.SignPolicyAsync(id, userid);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePolicy(string id)
        {
            try
            {
                await _policiesService.DeletePolicyAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
