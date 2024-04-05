using final_project_server.Models.Politics;
using final_project_server.Services.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace final_project_server.Services.Data.Repositories.Policies
{
    public class PolicyRepositoryEF : IPolicyRepository
    {
        private readonly ApplicationDbContext _context;

        public PolicyRepositoryEF(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePolicyAsync(ProjectPolicySQL policy)
        {
            var newPolicy = await _context.Policies.FirstOrDefaultAsync(p => p.Title == policy.Title);
            if (newPolicy != null)
            {
                return false;
            }
            await _context.Policies.AddAsync(policy);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ProjectPolicySQL> GetPolicyAsync(string id)
        {
            ProjectPolicySQL? policy = await _context.Policies.FindAsync(id);
            if (policy == null)
            {
                return null;
            }
            return policy;
        }

        public async Task<List<ProjectPolicySQL>> GetPoliciesAsync()
        {
            List<ProjectPolicySQL> policies = await _context.Policies.ToListAsync();
            return policies;
        }

        public async Task<ProjectPolicySQL> UpdatePolicyAsync(string id, ProjectPolicySQL updatedPol)
        {
            ProjectPolicySQL? oldPolicy = await _context.Policies.FindAsync(id);
            if (oldPolicy == null)
            {
                return null;
            }
            oldPolicy.Title = updatedPol.Title;
            oldPolicy.Description = updatedPol.Description;
            oldPolicy.Subtitle = updatedPol.Subtitle;
            await _context.SaveChangesAsync();
            return updatedPol;
        }

        public async Task<bool> DeletePolicyAsync(string id)
        {
            ProjectPolicySQL? policy = await _context.Policies.FindAsync(id);
            if (policy == null)
            {
                return false;
            }
            _context.Policies.Remove(policy);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SignPolicy(string policyId, string userId)
        {
            ProjectPolicySign? policySignCheck = await _context.PolicySigners.FirstOrDefaultAsync(x => x.PolicyId == policyId && x.UserId == userId);
            if (policySignCheck == null)
            {
                ProjectPolicySign newSign = new ProjectPolicySign { PolicyId = policyId, UserId = userId };
                await _context.PolicySigners.AddAsync(newSign);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                _context.PolicySigners.Remove(policySignCheck);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        //public async Task<bool> UnsignPolicy(string policyId, string userId)
        //{
        //    ProjectPolicySign? policySignCheck = await _context.PolicySigners.FirstOrDefaultAsync(x => x.PolicyId == policyId && x.UserId == userId);
        //    if (policySignCheck != null)
        //    {
        //         _context.PolicySigners.Remove(policySignCheck);
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    return false;
        //}
    }
}
