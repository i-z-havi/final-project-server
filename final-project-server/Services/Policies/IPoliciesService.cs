﻿using final_project_server.Models.Politics;
using final_project_server.Models.Politics.Policy_Models;

namespace final_project_server.Services.Policies
{
    public interface IPoliciesService
    {
        Task<ProjectPolicyNormalized> CreatePolicyAsync(ProjectPolicyNormalized policy);

        Task<ProjectPolicyNormalized> GetPolicyAsync(string id);

        Task<List<ProjectPolicyNormalized>> GetPoliciesAsync();

        Task<List<ProjectPolicyNormalized>> GetMyPoliciesAsync(string userId);

        Task<List<ProjectPolicyNormalized>> GetPendingPoliciesAsync();

        Task<ProjectPolicyNormalized> UpdatePolicyAsync(string id, ProjectPolicyNormalized updatedPol);

        Task AllowPolicyAsync(string policyId);

        Task DeletePolicyAsync(string id);

        Task SignPolicyAsync(string policyId, string userId);
    }
}
