using Exchange.Helpers;
using Exchange.Helpers.Interfaces;
using Exchange.Logic;
using Exchange.Logic.Interfaces;
using Exchange.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

internal class Program
{

    static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Debug()
              .WriteTo.Console()
              .WriteTo.File("logs/reportgenerator.txt")
              .CreateLogger();

        // Create a generic host
        Log.Information("Starting up the service...");
        var host = Host.CreateDefaultBuilder(args)
                    .UseSerilog() // Use Serilog for logging
                    .ConfigureServices((context, services) =>
                    {
                        services.AddTransient<IInputHandler, InputHandler>();
                        services.AddTransient<SharedInput>();
                        services.AddTransient<IApiGetter, ApiGetter>();
                        services.AddTransient<IExchangeCalculator, ExchangeCalculator>();
                    })
                       .Build();
        // Resolve the App class and run it
        try
        {
            var RatesGetterService = host.Services.GetRequiredService<IApiGetter>();
            var ExchangeCalculatorService = host.Services.GetRequiredService<IExchangeCalculator>();
            var ActualRates = (await RatesGetterService.GetRates());
            Console.WriteLine("Rates downloaded successfully");
            bool exit = false;
            do
            {
                Console.WriteLine("Usage: [amount] [input currency]/[output currency]. 'Exit' to Close");
                var input = Console.ReadLine();
                char[] delimiters = { '/', ' ' };
                var sharedInput = host.Services.GetRequiredService<SharedInput>();
                var InputHandlerService = host.Services.GetRequiredService<IInputHandler>();
                sharedInput.InputItems = InputHandlerService.SplitInput(input);
                if (sharedInput.InputItems[0].Equals("Exit"))
                {
                    exit = true;
                    Environment.Exit(0);
                }
                var InputAmount = InputHandlerService.ConvertAmount(sharedInput.InputItems[0]);
                var Result = ExchangeCalculatorService.RateCalculator(sharedInput.InputItems[1], sharedInput.InputItems[2], ActualRates);

                Console.WriteLine(ExchangeCalculatorService.AmountCalculator(InputAmount, Result));

            } while (!exit);

        }

        catch (Exception ex)
        {
            Console.WriteLine($"Custom Exception Caught: {ex.Message}");
        };
    }
}