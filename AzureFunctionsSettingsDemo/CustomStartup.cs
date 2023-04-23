using Azure.Identity;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(MyNamespace.Startup))]

namespace MyNamespace
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
   
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            // Build normal config so I can read key vault uri
            var builtConfig = builder.ConfigurationBuilder.Build();

            var keyVaultEndpoint = builtConfig["KeyVaultUri"];
            var tenantId = builtConfig["tenantId"];
            var clientId = builtConfig["clientId"];
            var clientSecret = builtConfig["clientSecret"];
            var multiplier = builtConfig["CustomMultiplier"];

            var creds = new ClientSecretCredential(tenantId, clientId, clientSecret);

            // Add everything - not sure if I need all
            var final = builder.ConfigurationBuilder
                .AddAzureKeyVault(new Uri(keyVaultEndpoint), creds)
                .Build();
            var multiplier2 = final["CustomMultiplier"];
        }
    }
}