using System;
using System.Linq;
using Sage.Cloud.Domain.Customers.Interfaces.Models;

namespace BlobUploadTester
{
    internal static class Program
    {
        private static readonly Guid TenantId = new Guid("3813fccf-4946-43e8-ac72-0c00d2df9f6f");

        private static void Main(string[] args)
        {
            var count = 0;

            if (args.Count() == 1)
                count = Convert.ToInt32(args[0]);

            while (count == 0)
            {
                Console.Write("Enter number of customers to create: ");
                if (!Int32.TryParse(Console.ReadLine(), out count))
                {
                    count = (-1);
                }
                if (count == 0) return;
            }

            var customerPrefix = "";

            if (args.Count() == 2)
                customerPrefix = args[2];

            while (string.IsNullOrEmpty(customerPrefix))
            {
                 Console.Write("Enter the customer name prefix: ");
                customerPrefix = Console.ReadLine();
            }

            long st = Environment.TickCount;

            Utility.GenerateContent(count, customerPrefix);
            Utility.UploadContent(TenantId);

            long et = Environment.TickCount - st;

            Console.WriteLine();
            Console.WriteLine("Elapsed time: {0:N0} ms", et);
            Console.WriteLine();

            Console.WriteLine("Press ENTER to quit");
            Console.ReadLine();
        }
    }
}