using System.Collections.Frozen;

namespace Exchange.Models
{
    public class Rates
    {
        public FrozenDictionary<string, decimal> MyFrozenDictionary { get; }

        // Create the FrozenDictionary from a source dictionary
        public Rates(IDictionary<string, decimal> sourceDictionary) =>
            MyFrozenDictionary = sourceDictionary.ToFrozenDictionary();
    }
}
