using System.Collections.Frozen;

namespace Exchange.Logic.Interfaces
{
    public interface IExchangeCalculator
    {
        public decimal AmountCalculator(decimal originalAmount, decimal rate);
        public decimal RateCalculator(string inputCurrency, string OutputCurrency, FrozenDictionary<string, decimal> FrozenRates);


    }
}
