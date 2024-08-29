namespace Exchange.Models
{
    public class SharedInput
    {
        public string[] InputItems { get; set; } = new string[3];
        public List<ExchangeRequest> ExchangeRequests { get; set; } = new List<ExchangeRequest>();

    }
}
