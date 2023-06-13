using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace LearnFuncApp
{
    public class QueueToDataInsertFunc
    {
        [FunctionName("QueueToDataInsertFunc")]
        public void Run([QueueTrigger("HttpToQueueRequestInBound", Connection = "AzureWebJobsStorage")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
