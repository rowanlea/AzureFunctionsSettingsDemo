using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Threading.Tasks;

namespace AzureFunctionsSettingsDemo
{
    public static class Multiply
    {
        [FunctionName("Multiply")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            int baseMultiplier = 1;
            
            var fromSettings = Environment.GetEnvironmentVariable("CustomMultiplier");
            Int32.TryParse(fromSettings, out baseMultiplier);

            int number = Convert.ToInt32(req.Query["number"]);
            return new OkObjectResult($"Multiplied number is: {number * baseMultiplier}");
        }
    }
}
