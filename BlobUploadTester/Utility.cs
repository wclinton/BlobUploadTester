using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.WindowsAzure.Storage.Blob;
using Sage.Connector.Customers.Contracts.CloudIntegration.Responses;
using Sage.Connector.Customers.Contracts.Data;
using Sage.Connector.Sync.Contracts.Data;


namespace BlobUploadTester
{
    public static class Utility
    {
        public static void UploadContent(Guid tenantId)
        {          
            long st = Environment.TickCount;

            Console.WriteLine("Starting blob upload...");


            var blobService = new BlobService(tenantId);
            


            var files = from f in Directory.EnumerateFiles(Directory.GetCurrentDirectory())
                where f.Contains("invoices.json.")
                select f;

   //         var i = 0;
            foreach (var path in files)
            {
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    var uploadId = Guid.NewGuid();
                    var blobUrl = blobService.GetBlobUploadUrl(uploadId);

                    Console.WriteLine("Uploading blob to:"+blobUrl);

                    var blob = new CloudBlockBlob(blobUrl);
                    
//                    i++;
//                    try
//                    {
//                        blob.Delete();
//                        Console.WriteLine("Deleted existing blob...");
//                    }
//                        // ReSharper disable once EmptyGeneralCatchClause
//                    catch (Exception)
//                    {
//
//                    }


                    blob.ServiceClient.ParallelOperationThreadCount = 4;
                    blob.UploadFromStream(stream);

                    blobService.CompleteBlobUpload(uploadId);
                }
            }

            blobService.CompleteAllUploads();

            long et = Environment.TickCount - st;

            Console.WriteLine("BlobService upload complete: {0:N0} ms (ParallelOperationThreadCount = 4)", et);
        }

        public static void GenerateContent(int count, string prefix)
        {
            Console.WriteLine("Starting content generation...");

            var files = from f in Directory.EnumerateFiles(Directory.GetCurrentDirectory())
                where f.Contains("invoices.json.")
                select f;

            foreach (var file in files)
            {
                File.Delete(file);
            }

            long st = Environment.TickCount;

            var callback = new HandlerCallback();

            var streamer = new JsonContentListStreamer(null, callback, Guid.NewGuid());

            int tick = 1;

            var sync = GetSyncCustomerResponse(tick++);

            sync.SyncDigest = new SyncDigest { EndpointId = 0, EndpointTick = 1, ResourceKindName = "Customer" };

            for (var i = 0; i < count; i++)
            {
                var customer = CustomerUtil.CreateCustomer(prefix,i);             
                sync.Customers.Add(customer);

                //Add up to 100 customers in a sync block
                if (sync.Customers.Count >= 100)
                {
                    streamer.Add(sync);
                    sync = GetSyncCustomerResponse(tick++);
                }
            }
            //Push the remaining sync block to blob.

            if(sync.Customers.Count>0)
                streamer.Add(sync);
            streamer.Flush(true);

            long et = Environment.TickCount - st;

            Console.WriteLine("Content generation time: {0:N0} ms", et);
        }

        private static SyncCustomersResponse GetSyncCustomerResponse(int tick)
        {
            var sync = new SyncCustomersResponse
            {
                SyncDigest = new SyncDigest {EndpointId = 0, EndpointTick = tick, ResourceKindName = "Customer"},
                Customers = new List<Customer>()
            };

            return sync;
        }
    }
}