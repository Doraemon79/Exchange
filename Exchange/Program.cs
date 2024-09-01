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

        Console.WriteLine("Usage: [amount] [input currency]/[output currency]. 'Exit' to Close  ");
        Console.WriteLine("N.B. use ',' to separate the decimal from the integer part ");
        Console.WriteLine("  use ';' to separate different requests E.g.: 100 USD/EUR;20,6 GBP/DKK; 33,45 EUR/USD ");
        while (!exit)
        {
            try
            {
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
                List<ExchangeRequest> requests = InputHandlerService.SplitInput(input);
                foreach (var r in requests)
                {
                    var InputAmount = r.OriginalAmount;

                    //these 2 clauses are unencessary, in case of outputcurrency equals inputcurrency the
                    //rate calculator would return 1 but it is here to avoid the (small) calculation
                    if (r.InputCurrency.Equals(r.OutputCurrency))
                    {
                        Console.WriteLine(ExchangeCalculatorService.AmountCalculator(InputAmount, 1).ToString().Replace('.', ',') + "   " + r.OutputCurrency);
                    }
                    if (InputAmount == 0)
                    {
                        Console.WriteLine("Amount is 0, no conversion needed");
                    }
                    else
                    {
                        var Rate = ExchangeCalculatorService.RateCalculator(r.InputCurrency, r.OutputCurrency, frozenRates.MyFrozenDictionary);
                        Console.WriteLine(ExchangeCalculatorService.AmountCalculator(InputAmount, Rate).ToString().Replace('.', ',') + "   " + r.OutputCurrency);
                    }

                }
            }

            catch (CustomException ex)
            {
                Console.WriteLine($"Custom Exception Caught: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Custom Exception Caught: {ex.Message}");
            }
        }
    }
}
