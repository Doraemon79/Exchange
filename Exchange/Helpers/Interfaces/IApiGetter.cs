namespace Exchange.Helpers.Interfaces
{
    public interface IApiGetter
    {
        Task<Dictionary<string, decimal>> GetRates();
    }
}
