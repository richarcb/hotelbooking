using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace BookingService.Services


{
    public class KeyVaultService : IKeyVaultService
    {
        private readonly IConfiguration _configuration;
        private readonly string _keyVaultUri;

        public KeyVaultService(IConfiguration configuration)
        {
            _configuration = configuration;
            _keyVaultUri = _configuration["KeyVaultUri"];
        }
        public string GetSecret(string name)
        {
            var client = new SecretClient(new Uri(_keyVaultUri), new DefaultAzureCredential());
            KeyVaultSecret secret = client.GetSecret(name);
            return secret.Value;
        }
    }
}
