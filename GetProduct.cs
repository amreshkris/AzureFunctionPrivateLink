using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Contoso.Product
{
    public static class GetProduct
    {
        [FunctionName("GetProduct")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "ProductsCatalog",
                collectionName: "Products",
                ConnectionStringSetting = "CosmosDBConnectionString",
                Id = "{Query.id}",
                PartitionKey = "{Query.partitionKey}")] Product product,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (product == null)
            {
                log.LogInformation($"product not found");
            }
            else
            {
                log.LogInformation($"Found ToDo item, Description={product.name}");
            }
            return new OkObjectResult(product);            
        }
    }
}
