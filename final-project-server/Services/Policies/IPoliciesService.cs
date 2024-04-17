using final_project_server.Models.Politics;
using final_project_server.Models.Politics.Policy_Models;

namespace final_project_server.Services.Policies
{
	public interface IPoliciesService
	{
		Task<ProjectPolicyNormalized> CreatePolicyAsync(ProjectPolicyNormalized policy);

		Task<ProjectPolicyNormalized> GetPolicyAsync(string id);

		Task<List<ProjectPolicyNormalized>> GetPoliciesAsync();

		Task<ProjectPolicyNormalized> UpdatePolicyAsync(string id, ProjectPolicyNormalized updatedPol);

		Task DeletePolicyAsync(string id);

		Task SignPolicyAsync(string policyId, string userId);
	}
}
