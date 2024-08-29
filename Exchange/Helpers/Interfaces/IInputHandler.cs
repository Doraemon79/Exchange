using Exchange.Models;

namespace Exchange.Helpers.Interfaces
{
    public interface IInputHandler
    {
        List<ExchangeRequest> SplitInput(string input);
        decimal ConvertAmount(string amount);

    }
}
