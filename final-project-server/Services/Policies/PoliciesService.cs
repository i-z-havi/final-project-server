using final_project_server.Models.Politics;
using final_project_server.Models.Politics.Policy_Models;
using final_project_server.Services.Data.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace final_project_server.Services.Policies
{
    public class PoliciesService : IPoliciesService
    {
        public IPolicyRepository _policies;

        public PoliciesService(IPolicyRepository repo)
        {
            _policies = repo;
        }

        //create
        public async Task<ProjectPolicyNormalized> CreatePolicyAsync(ProjectPolicyNormalized policy)
        {
            bool result = await _policies.CreatePolicyAsync(policy);
            if (result == true)
            {
                return policy;
            }
            else
            {
                throw new Exception("Policy already exists!");
            }
        }

        //read one 
        public async Task<ProjectPolicyNormalized> GetPolicyAsync(string id)
        {
            ProjectPolicyNormalized pol = await _policies.GetPolicyAsync(id);
            return pol;
        }

        //read all
        public async Task<List<ProjectPolicyNormalized>> GetPoliciesAsync()
        {
            return await _policies.GetPoliciesAsync();
        }

        public async Task<List<ProjectPolicyNormalized>> GetMyPoliciesAsync(string userId)
        {
            return await _policies.GetMyPoliciesAsync(userId);
        }


        public async Task<List<ProjectPolicyNormalized>> GetPendingPoliciesAsync()
        {
            return await _policies.GetPendingPoliciesAsync();
        }

        //update 
        public async Task<ProjectPolicyNormalized> UpdatePolicyAsync(string id, ProjectPolicyNormalized updatedPol)
        {
            ProjectPolicyNormalized pol = await _policies.GetPolicyAsync(id);
            if (pol == null)
            {
                throw new Exception("Policy not found!");
            }
            ProjectPolicyNormalized newUpdatedPol = await _policies.UpdatePolicyAsync(id, updatedPol);
            if (newUpdatedPol==null)
            {
                throw new Exception("Another policy has this title!");
            }
            return newUpdatedPol;
        }

        //delete
        public async Task DeletePolicyAsync(string id)
        {
            bool result = await _policies.DeletePolicyAsync(id);
            if (result == false)
            {
                throw new Exception("Policy not found!");
            }
        }

        public async Task SignPolicyAsync(string policyId, string userId)
        {
            ProjectPolicyNormalized pol = await _policies.GetPolicyAsync(policyId);
            if (pol == null)
            {
                throw new Exception("No policy found!");
            }
            await _policies.SignPolicy(policyId, userId);
        }

        public async Task AllowPolicyAsync(string policyId)
        {
            ProjectPolicyNormalized pol = await _policies.GetPolicyAsync(policyId);
            if (pol == null)
            {
                throw new Exception("No policy found!");
            }
            await _policies.AllowPolicy(policyId);
        }
    }
}
