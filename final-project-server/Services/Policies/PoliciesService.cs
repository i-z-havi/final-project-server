using final_project_server.Models.Politics;
using MongoDB.Bson;
using MongoDB.Driver;

namespace final_project_server.Services.Policies
{
    public class PoliciesService : IPoliciesService
    {
        public IMongoCollection<Models.Politics.ProjectPolicy> _policies;

        public PoliciesService(IMongoClient mongoClient)
        {
            var dataBase = mongoClient.GetDatabase("policies_website");
            _policies = dataBase.GetCollection<Models.Politics.ProjectPolicy>("policies");
        }

        //create
        public async Task<Models.Politics.ProjectPolicy> CreatePolicyAsync(Models.Politics.ProjectPolicy policy)
        {
            var check = await _policies.Find(u => u.Title == policy.Title).FirstOrDefaultAsync();
            if (check != null)
            {
                throw new Exception("This policy already exists!");
            }
            await _policies.InsertOneAsync(policy);
            return policy;
        }

        //read one 
        public async Task<Models.Politics.ProjectPolicy> GetPolicyAsync(string id)
        {
            Models.Politics.ProjectPolicy pol = await _policies.Find(p => p.Id.ToString() == id).FirstOrDefaultAsync();
            if (pol == null)
            {
                throw new Exception("Policy not found!");
            }
            return pol;
        }

        //read all
        public async Task<List<Models.Politics.ProjectPolicy>> GetPoliciesAsync()
        {
            return await _policies.Find(_ => true).ToListAsync();
        }

        //update 
        public async Task UpdatePolicyAsync(string id, Models.Politics.ProjectPolicy updatedPol)
        {
            var filter = Builders<Models.Politics.ProjectPolicy>.Filter.Eq(p => p.Id, new ObjectId(id));
            var builder = Builders<Policies>.Update
                .Set(p => p.PoliticalLean, updatedPol.PoliticalLean)
                .Set(p => p.Title, updatedPol.Title)
                .Set(p => p.Subtitle, updatedPol.Subtitle)
                .Set(p => p.Description, updatedPol.Description);

            var count = await _policies.UpdateOneAsync(filter, builder);
            if (count.MatchedCount == 0)
            {
                throw new Exception("Policy not found!");
            }
        }

        //delete
        public async Task DeletePolicyAsync(string id)
        {
            var result = await _policies.DeleteOneAsync(p => p.Id.ToString() == id);
            if (result.DeletedCount == 0)
            {
                throw new Exception("Policy not found!");
            }
        }
    }
}
