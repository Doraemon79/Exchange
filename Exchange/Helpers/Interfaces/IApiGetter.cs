using System.Collections.Frozen;

namespace Exchange.Helpers.Interfaces
{
    public interface IApiGetter
    {
        Task<FrozenDictionary<string, decimal>> GetRates();
    }
}
