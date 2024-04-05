using final_project_server.Models.Politics;

namespace final_project_server.Services.Data.Repositories.Interfaces
{
	public interface IPolicyRepository
	{
		Task<bool> CreatePolicyAsync(ProjectPolicySQL policy);

		Task<ProjectPolicySQL> GetPolicyAsync(string id);

		Task<List<ProjectPolicySQL>> GetPoliciesAsync();

		Task<ProjectPolicySQL> UpdatePolicyAsync(string id, ProjectPolicySQL updatedPol);

		Task<bool> DeletePolicyAsync(string id);

		Task<bool> SignPolicy(string policyId, string userId);
		
		//Task<bool> UnsignPolicy(string policyId, string userId);
	}
}
