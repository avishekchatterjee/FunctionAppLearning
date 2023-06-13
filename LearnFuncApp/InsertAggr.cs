using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace LearnFuncApp
{
    public class InsertAggr
    {
        private readonly ILogger<InsertAggr> _logger;

        public InsertAggr(ILogger<InsertAggr> log)
        {
            _logger = log;
        }

        [FunctionName("InsertAggr")]
        public void Run([ServiceBusTrigger("clsaggregationtopic", "subsclsaggregationtopic", Connection = "AzureWebJobsStorage")]string mySbMsg)
        {
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}
