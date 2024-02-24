using final_project_server.Models.Politics;

namespace final_project_server.Services.Policies
{
	public interface IPoliciesService
	{
		Task<ProjectPolicy> CreatePolicyAsync(ProjectPolicy policy);

		Task<ProjectPolicy> GetPolicyAsync(string id);

		Task<List<ProjectPolicy>> GetPoliciesAsync();

		Task UpdatePolicyAsync(string id, ProjectPolicy	 updatedPol);

		Task DeletePolicyAsync(string id);
	}
}
