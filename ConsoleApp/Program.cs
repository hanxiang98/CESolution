using CE.Service;
using CE.Service.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddTransient<IOrderService, OrderService>()
                .BuildServiceProvider();


            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            //do the actual work here
            var service = serviceProvider.GetService<IOrderService>();

            // Display title as the C# console calculator app.
            Console.WriteLine("Channel Engine\r");
            Console.WriteLine("------------------------\n");
            var isContinue = "n";
            do
            {
                // Ask the user to type the first number.
                Console.WriteLine("Load all in progress orders");

                var orders = await service.GetInProgressOrdersAsync();

                foreach (var order in orders)
                {
                    string orderStr = JsonConvert.SerializeObject(order);
                    Console.WriteLine(orderStr);
                }

                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\ta - Show top 5 products sold.");
                Console.WriteLine("\tb - Set the stock of a product to 25.");
                Console.Write("Your option? ");

                // Use a switch statement to do the math.
                switch (Console.ReadLine())
                {
                    case "a":
                        var top5productsSold = await service.GetTopFiveProductsSold(orders);
                        foreach (var productSold in top5productsSold)
                        {
                            string productSoldStr = JsonConvert.SerializeObject(productSold);
                            Console.WriteLine(productSoldStr);
                        }
                        break;
                    case "b":
                        Console.WriteLine("Enter the merchant product number that you wish to update the stock to 25.");
                        Console.Write("merchant product number: ");
                        var productNo = Console.ReadLine();
                        await service.UpdateProductStock(productNo);
                        break;
                }
                await Task.WhenAll();
                Console.WriteLine("Do you wish to continue? (y/n)");
                isContinue = Console.ReadLine();
            }
            while (isContinue == "y");



            Console.Write("Press any key to close the Calculator console app...");
            Console.ReadKey();
        }
    }
}
