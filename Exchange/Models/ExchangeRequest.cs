namespace Exchange.Models
{
    public class ExchangeRequest
    {
        public string InputCurrency { get; set; }
        public string OutputCurrency { get; set; }
        public decimal OriginalAmount { get; set; }

    }
}
