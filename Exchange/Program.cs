using Exchange.Exceptions;
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

        var exit = false;


        var RatesGetterService = host.Services.GetRequiredService<IApiGetter>();
        var ActualRates = await RatesGetterService.GetRates();

        var frozenRates = new Rates(ActualRates);
        Console.WriteLine("Rates downloaded successfully");


        while (!exit)
        {
            try
            {
                Console.WriteLine("Usage: [amount] [input currency]/[output currency]. 'Exit' to Close  ");
                Console.WriteLine("Usage: use ',' to separate the decimal from the integer part ");

                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    throw new CustomException("Input is meaningless please try again.");
                }
                if (input.Equals("Exit"))
                {
                    exit = true;
                    Environment.Exit(0);
                }

                //call th services
                var sharedInput = host.Services.GetRequiredService<SharedInput>();
                var InputHandlerService = host.Services.GetRequiredService<IInputHandler>();
                var ExchangeCalculatorService = host.Services.GetRequiredService<IExchangeCalculator>();


                //here it is all the logic
                var requests = InputHandlerService.SplitInput(input);
                foreach (var r in requests)
                {
                    var InputAmount = r.OriginalAmount;
                    var Result = ExchangeCalculatorService.RateCalculator(r.InputCurrency, r.OutputCurrency, frozenRates.MyFrozenDictionary);
                    Console.WriteLine(ExchangeCalculatorService.AmountCalculator(InputAmount, Result).ToString().Replace('.', ','));
                }

            }
            catch (CustomException ex)
            {
                Console.WriteLine($"Custom Exception Caught: {ex.Message}");
            }

        }
    }
}
