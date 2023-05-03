using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace AZFunctionsSettingsDemo
{
    public class Function1
    {
        private readonly IConfiguration _configuration = null;

        public Function1(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [FunctionName("Function1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            int baseMultiplier = 1;

            var keyVaultEndpoint = _configuration["KeyVaultUri"];
            var tenantId = _configuration["tenantId"];
            var clientId = _configuration["clientId"];
            var clientSecret = _configuration["clientSecret"];

            var creds = new ClientSecretCredential(tenantId, clientId, clientSecret);

            var client = new SecretClient(vaultUri: new Uri(keyVaultEndpoint), credential: creds);
            KeyVaultSecret secret = client.GetSecret("CustomMultiplier");

            Int32.TryParse(secret.Value, out baseMultiplier);

            int number = Convert.ToInt32(req.Query["number"]);
            return new OkObjectResult($"Multiplied number is: {number * baseMultiplier}");
        }
    }
}
