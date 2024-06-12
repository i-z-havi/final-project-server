using final_project_server.Models.Politics;
using final_project_server.Models.Politics.Policy_Models;

namespace final_project_server.Services.Data.Repositories.Interfaces
{
    public interface IPolicyRepository
    {
        Task<bool> CreatePolicyAsync(ProjectPolicyNormalized policy);

        Task<ProjectPolicyNormalized> GetPolicyAsync(string id);

        Task<List<ProjectPolicyNormalized>> GetPoliciesAsync();

        Task<List<ProjectPolicyNormalized>> GetMyPoliciesAsync(string userId);

        Task<List<ProjectPolicyNormalized>> GetPendingPoliciesAsync();

        Task<ProjectPolicyNormalized> UpdatePolicyAsync(string id, ProjectPolicyNormalized updatedPol);

        Task<bool> AllowPolicy(string policyId);

        Task<bool> DeletePolicyAsync(string id);

        Task<bool> SignPolicy(string policyId, string userId);

        //Task<bool> UnsignPolicy(string policyId, string userId);
    }
}
