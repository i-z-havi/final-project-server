using final_project_server.Models.Politics;
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
        public async Task<ProjectPolicySQL> CreatePolicyAsync(ProjectPolicySQL policy)
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
        public async Task<ProjectPolicySQL> GetPolicyAsync(string id)
        {
            ProjectPolicySQL pol = await _policies.GetPolicyAsync(id);
            if(pol == null)
            {
                throw new Exception("Policy not found!");
            }
            return pol;
        }

        //read all
        public async Task<List<ProjectPolicySQL>> GetPoliciesAsync()
        {
            return await _policies.GetPoliciesAsync();
        }

        //update 
        public async Task<ProjectPolicySQL> UpdatePolicyAsync(string id, ProjectPolicySQL updatedPol)
        {
            ProjectPolicySQL pol = await _policies.GetPolicyAsync(id);
            if (pol==null)
            {
                throw new Exception("Policy not found!");
            }
            ProjectPolicySQL newUpdatedPol = await _policies.UpdatePolicyAsync(id, updatedPol);
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
            ProjectPolicySQL pol = await _policies.GetPolicyAsync(policyId);
            if (pol==null)
            {
                throw new Exception("No policy found!");
            }
            await _policies.SignPolicy(policyId, userId);
        }
    }
}
