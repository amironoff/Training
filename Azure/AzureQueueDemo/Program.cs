using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

namespace AzureQueueDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //for (int i = 0; i < 100; i++)
            //{
            //    Console.WriteLine("Queue created: ", CreateAzureQueueAsync().Result);
            //    EnqueueMessageAsync().Wait();
            //    Console.WriteLine("Message sent");
            //}
            ReadMessagesAsync();


            Console.Read();
        }

        static async Task<bool> CreateAzureQueueAsync()
        {
            var storageCredentials = new StorageCredentials("alphaq", "cNLzSGVT7qumzhy3EXNCDtVTbQN96hoMRAEcIxDLkj8Ki50jmPYIqgBU8FuYR80NkfQFkwDf/iXdwnYejAv6VA==");
            var storageAccount = new CloudStorageAccount(storageCredentials, true);
            var queueClient = storageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference("product-harvest");

            return await queue.CreateIfNotExistsAsync();
        }

        static async Task EnqueueMessageAsync()
        {
            var storageCredentials = new StorageCredentials("alphaq", "cNLzSGVT7qumzhy3EXNCDtVTbQN96hoMRAEcIxDLkj8Ki50jmPYIqgBU8FuYR80NkfQFkwDf/iXdwnYejAv6VA==");
            var storageAccount = new CloudStorageAccount(storageCredentials, true);
            var queueClient = storageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference("product-harvest");
            var generator = new Random();
            var request = new ProductImportRequest()
            {
                ImportAction = ImportAction.Create,
                Product = new EnqueuedProduct()
                {
                    ProductName = "Azure Queue Subscription " + generator.Next(),
                    Description = "A nice non-expiring subscription",
                    Price = Decimal.One,
                    Id = generator.Next().ToString()
                }
            };

            var ticketJson = await JsonConvert.SerializeObjectAsync(request);
            var message = new CloudQueueMessage(ticketJson);

            await queue.AddMessageAsync(message);
        }

        static CloudQueue GetQueue()
        {
            var storageCredentials = new StorageCredentials("alphaq", "cNLzSGVT7qumzhy3EXNCDtVTbQN96hoMRAEcIxDLkj8Ki50jmPYIqgBU8FuYR80NkfQFkwDf/iXdwnYejAv6VA==");
            var storageAccount = new CloudStorageAccount(storageCredentials, true);
            var queueClient = storageAccount.CreateCloudQueueClient();
            return queueClient.GetQueueReference("product-harvest");
        }

        static async Task ReadMessagesAsync()
        {
            CloudQueue queue = GetQueue();
            var messages = await queue.GetMessagesAsync(32, TimeSpan.FromMinutes(1), null, null);
            foreach (CloudQueueMessage message in messages)
            {
                try
                {

                    string rawString = message.GetPrivatePropertyValue<string>("RawString");
                    // if processing was not possible, delete the messagecheck for unprocessable messages
                    if (message.DequeueCount < 5)
                    {
                        var ticketRequest = await JsonConvert.DeserializeObjectAsync<ProductImportRequest>(rawString);
                        // process the ticket request (expensive operation)
                        ProcessTicket(ticketRequest);
                    }
                    else
                    {
                        Console.WriteLine("{0}{1}",message, "Processing failed.");
                    }
                    // delete message so that it becomes invisible for other workers
                    await queue.DeleteMessageAsync(message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0}{1}", message, e.Message);
                }
            }
        }

        private static void ProcessTicket(ProductImportRequest ticketRequest)
        {
            Console.WriteLine("Received ticket request from {0}", ticketRequest.Product.ProductName);
        }
    }
}
