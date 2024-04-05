using final_project_server.Models.Politics;

namespace final_project_server.Services.Policies
{
	public interface IPoliciesService
	{
		Task<ProjectPolicySQL> CreatePolicyAsync(ProjectPolicySQL policy);

		Task<ProjectPolicySQL> GetPolicyAsync(string id);

		Task<List<ProjectPolicySQL>> GetPoliciesAsync();

		Task<ProjectPolicySQL> UpdatePolicyAsync(string id, ProjectPolicySQL updatedPol);

		Task DeletePolicyAsync(string id);
	}
}
