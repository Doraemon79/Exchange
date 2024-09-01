using Exchange.Exceptions;
using Exchange.Logic.Interfaces;
using System.Collections.Frozen;

namespace Exchange.Logic
{
    public class ExchangeCalculator : IExchangeCalculator
    {
        public decimal AmountCalculator(decimal originalAmount, decimal rate)
        {
            //this 2 clauses should never be needed but it is good practice to check for invalid inputs
            if (!(originalAmount >= 0))
            {
                throw new CustomException("The amount must be a positive number.");
            }
            if (!(rate > 0))
            {
                throw new CustomException("The rate must be a positive number.");
            }
            return originalAmount * rate;
        }

        public decimal RateCalculator(string InputCurrency, string OutputCurrency, FrozenDictionary<string, decimal> FrozenRates)
        {
            if (!FrozenRates.TryGetValue(OutputCurrency, out decimal outputRate))
            {
                throw new KeyNotFoundException($"The currency '{OutputCurrency}' was not found in the dictionary.");
            }
            if (!FrozenRates.TryGetValue(InputCurrency, out decimal inputRate))
            {
                throw new KeyNotFoundException($"The currency '{InputCurrency}' was not found in the dictionary.");
            }

            //Check if the rates are valid this clause is not really needed but it is
            //a good practice to check for invalid rates 
            if (!(inputRate > 0) || !(outputRate > 0))
            {
                throw new CustomException("The rates must be   positive numbers.");
            }
            //Default reference currency in freecurrencyapi is USD
            if (InputCurrency == "USD")
            {
                return FrozenRates[OutputCurrency];
            }

            return outputRate / inputRate;
        }
    }
}
