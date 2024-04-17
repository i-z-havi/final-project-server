﻿using final_project_server.Models.Politics;
using final_project_server.Models.Politics.Policy_Models;

namespace final_project_server.Services.Data.Repositories.Interfaces
{
	public interface IPolicyRepository
	{
		Task<bool> CreatePolicyAsync(ProjectPolicyNormalized policy);

		Task<ProjectPolicyNormalized> GetPolicyAsync(string id);

		Task<List<ProjectPolicyNormalized>> GetPoliciesAsync();

		Task<ProjectPolicyNormalized> UpdatePolicyAsync(string id, ProjectPolicyNormalized updatedPol);

		Task<bool> DeletePolicyAsync(string id);

		Task<bool> SignPolicy(string policyId, string userId);
		
		//Task<bool> UnsignPolicy(string policyId, string userId);
	}
}
