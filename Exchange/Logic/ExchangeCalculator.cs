using Exchange.Logic.Interfaces;
using System.Collections.Frozen;

namespace Exchange.Logic
{
    public class ExchangeCalculator : IExchangeCalculator
    {
        public decimal AmountCalculator(decimal originalAmount, decimal rate)
        {
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
            if (InputCurrency == "DOL")
            {
                return FrozenRates[OutputCurrency];
            }

            return outputRate / inputRate;
        }
    }
}
