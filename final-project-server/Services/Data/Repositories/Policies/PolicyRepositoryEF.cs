using final_project_server.Models.Politics;
using final_project_server.Models.Politics.Policy_Models;
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

        public async Task<bool> CreatePolicyAsync(ProjectPolicyNormalized policy)
        {
            var newPolicy = await _context.Policies.FirstOrDefaultAsync(p => p.Title == policy.Title);
            if (newPolicy != null)
            {
                return false;
            }
            ProjectPolicySQL pol = new ProjectPolicySQL(policy);
            await _context.Policies.AddAsync(pol);
            if (policy.Details != null)
            {
                foreach (var detail in policy.Details)
                {
                    await _context.PolicyDetails.AddAsync(new PolicyDetails { Leaning = detail, PolicyId = pol.Id });
                }
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ProjectPolicyNormalized> GetPolicyAsync(string id)
        {
            ProjectPolicySQL? policy = await _context.Policies.FindAsync(id);

            if (policy == null)
            {
                return null;
            }

            List<PoliticalEnum>? details = await _context.PolicyDetails.Where(p => p.PolicyId == id).Select(p => p.Leaning).ToListAsync();
            List<string>? signatures = await _context.PolicySigners.Where(p => p.PolicyId == id).Select(p => p.UserId).ToListAsync();

            ProjectPolicyNormalized getPol = new ProjectPolicyNormalized(policy, details, signatures);
            return getPol;
        }

        public async Task<List<ProjectPolicyNormalized>> GetPoliciesAsync()
        {
            var policies = await _context.Policies.Where(p => p.IsActive == true).ToListAsync();
            Dictionary<string, List<string>> signaturesByPolicyId = await _context.PolicySigners
                                                            .GroupBy(sign => sign.PolicyId)
                                                            .ToDictionaryAsync(group => group.Key, group => group.Select((sign) => sign.UserId).ToList());

            Dictionary<string, List<PoliticalEnum>> detailsByPolicyID = await _context.PolicyDetails
                                                             .GroupBy(detail => detail.PolicyId)
                                                             .ToDictionaryAsync(group => group.Key, group => group.Select((detail) => detail.Leaning).ToList());

            return policies.Select(policySQL => new ProjectPolicyNormalized(policySQL,
                                detailsByPolicyID.GetValueOrDefault(policySQL.Id, new List<PoliticalEnum>()),
                                signaturesByPolicyId.GetValueOrDefault(policySQL.Id, new List<string>()))).ToList();
        }

        public async Task<List<ProjectPolicyNormalized>> GetMyPoliciesAsync(string userId)
        {
            var policies = await _context.Policies.Where(p => p.CreatorId == userId).ToListAsync();
            Dictionary<string, List<string>> signaturesByPolicyId = await _context.PolicySigners
                                                            .GroupBy(sign => sign.PolicyId)
                                                            .ToDictionaryAsync(group => group.Key, group => group.Select((sign) => sign.UserId).ToList());

            Dictionary<string, List<PoliticalEnum>> detailsByPolicyID = await _context.PolicyDetails
                                                             .GroupBy(detail => detail.PolicyId)
                                                             .ToDictionaryAsync(group => group.Key, group => group.Select((detail) => detail.Leaning).ToList());

            return policies.Select(policySQL => new ProjectPolicyNormalized(policySQL,
                                detailsByPolicyID.GetValueOrDefault(policySQL.Id, new List<PoliticalEnum>()),
                                signaturesByPolicyId.GetValueOrDefault(policySQL.Id, new List<string>()))).ToList();
        }

        public async Task<List<ProjectPolicyNormalized>> GetPendingPoliciesAsync()
        {
            var policies = await _context.Policies.Where(p => p.IsActive == false).ToListAsync();
            Dictionary<string, List<string>> signaturesByPolicyId = await _context.PolicySigners
                                                            .GroupBy(sign => sign.PolicyId)
                                                            .ToDictionaryAsync(group => group.Key, group => group.Select((sign) => sign.UserId).ToList());

            Dictionary<string, List<PoliticalEnum>> detailsByPolicyID = await _context.PolicyDetails
                                                             .GroupBy(detail => detail.PolicyId)
                                                             .ToDictionaryAsync(group => group.Key, group => group.Select((detail) => detail.Leaning).ToList());

            return policies.Select(policySQL => new ProjectPolicyNormalized(policySQL,
                               detailsByPolicyID.GetValueOrDefault(policySQL.Id, new List<PoliticalEnum>()),
                               signaturesByPolicyId.GetValueOrDefault(policySQL.Id, new List<string>()))).ToList();
        }

        public async Task<ProjectPolicyNormalized> UpdatePolicyAsync(string id, ProjectPolicyNormalized updatedPol)
        {
            ProjectPolicySQL? oldPolicy = await _context.Policies.FindAsync(id);
            List<PolicyDetails>? oldDetails = await _context.PolicyDetails.Where(d => d.PolicyId == id).ToListAsync();
            if (oldPolicy == null)
            {
                return null;
            }
            //update policy
            oldPolicy.Title = updatedPol.Title;
            oldPolicy.Description = updatedPol.Description;
            oldPolicy.Subtitle = updatedPol.Subtitle;
            //update details
            if (updatedPol.Details != null)
            {
                if (oldDetails != null)
                {
                    foreach (PolicyDetails detail in oldDetails)
                    {
                        _context.PolicyDetails.Remove(detail);
                    }
                }
                foreach (var detail in updatedPol.Details)
                {
                    await _context.PolicyDetails.AddAsync(new PolicyDetails { PolicyId = id, Leaning = detail });
                }
            }
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
