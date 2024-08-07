using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace final_project_server.Services
{
    public class KeyVaultService
    {
        private readonly SecretClient _secretClient;

        public KeyVaultService(IConfiguration configuration)
        {
            var vaultUri = configuration["KeyVault:VaultUri"];
            _secretClient = new SecretClient(new Uri(vaultUri), new DefaultAzureCredential());
        }

        public async Task<string> GetSecretAsync(string secretName)
        {
            var secret = await _secretClient.GetSecretAsync(secretName);
            return secret.Value.Value;
        }
    }
}
