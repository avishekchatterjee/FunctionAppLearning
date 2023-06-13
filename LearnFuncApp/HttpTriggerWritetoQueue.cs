using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using LearnFuncApp.Model;

namespace LearnFuncApp
{
    public static class HttpTriggerWritetoQueue
    {       
        [FunctionName("HttpTriggerWritetoQueue")]        
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Queue("HttpToQueueRequestInBound", Connection = "AzureWebJobsStorage")] IAsyncCollector<CustomerDetails> customerDetailsToQueue, // Queue setup to 
            ILogger log)
        {
            string responseMessage;
            try
            {
                log.LogInformation("Started :: HttpTriggerWritetoQueue Function");

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var customerDetails = JsonConvert.DeserializeObject<CustomerDetails>(requestBody);
                if (customerDetails != null)
                {
                    await customerDetailsToQueue.AddAsync(customerDetails); // This is sending object to Queue

                    responseMessage = "Customer Details request Sent to Queue." + customerDetails.CutomerName;
                }
                else
                {
                    responseMessage = "Customer Details not provided. Request Sent to Queue Failed.";
                }
            }
            catch(ArgumentException ex)
            {
                responseMessage = string.IsNullOrEmpty(ex.Message.ToString()) ? ex.InnerException.Message.ToString() : ex.Message.ToString();
            }
            return new OkObjectResult(responseMessage);
        }
    }
}
