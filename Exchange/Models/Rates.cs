using System.Collections.Frozen;

namespace Exchange.Models
{
    public class Rates
    {
        public FrozenDictionary<string, decimal> MyFrozenDictionary { get; }

        public Rates(IDictionary<string, decimal> sourceDictionary)
        {
            // Creating the FrozenDictionary from a source dictionary
            MyFrozenDictionary = sourceDictionary.ToFrozenDictionary();
        }
    }
}
