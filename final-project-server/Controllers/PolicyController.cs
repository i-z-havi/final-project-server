using final_project_server.Models.Politics;
using final_project_server.Services.Policies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace final_project_server.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class PolicyController : ControllerBase
	{
		private PoliciesService _policiesService;

		public PolicyController(IMongoClient policiesService)
		{
			_policiesService = new PoliciesService(policiesService);
		}

		[HttpGet]
		public async Task<IActionResult> GetPolicies()
		{
			List<ProjectPolicyMongo> policies = await _policiesService.GetPoliciesAsync();
			return Ok(policies);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetPolicy(string id)
		{
			ProjectPolicyMongo pol = await _policiesService.GetPolicyAsync(id);
			if (pol != null)
			{
				return Ok(pol);
			}
			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> CreatePolicy([FromBody] ProjectPolicyMongo policy)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			ProjectPolicyMongo pol = await _policiesService.CreatePolicyAsync(policy);
			return CreatedAtAction(nameof(GetPolicy), new { id = pol.Id }, pol);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdatePolicy(string id, [FromBody] ProjectPolicyMongo pol)
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
