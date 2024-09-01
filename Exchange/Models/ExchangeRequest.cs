using System.Diagnostics.CodeAnalysis;

namespace Exchange.Models
{
    public class ExchangeRequest
    {
        public required string InputCurrency { get; set; }
        public required string OutputCurrency { get; set; }
        public decimal OriginalAmount { get; set; }

        [SetsRequiredMembers]
        public ExchangeRequest(string inputCurrency, string outputCurrency, decimal originalAmount)
        {
            InputCurrency = inputCurrency;
            OutputCurrency = outputCurrency;
            OriginalAmount = originalAmount;
        }
    }
}
