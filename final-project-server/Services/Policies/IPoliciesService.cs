using final_project_server.Models.Politics;

namespace final_project_server.Services.Policies
{
	public interface IPoliciesService
	{
		Task<ProjectPolicyMongo> CreatePolicyAsync(ProjectPolicyMongo policy);

		Task<ProjectPolicyMongo> GetPolicyAsync(string id);

		Task<List<ProjectPolicyMongo>> GetPoliciesAsync();

		Task UpdatePolicyAsync(string id, ProjectPolicyMongo	 updatedPol);

		Task DeletePolicyAsync(string id);
	}
}
