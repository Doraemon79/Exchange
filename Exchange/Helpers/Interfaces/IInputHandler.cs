namespace Exchange.Helpers.Interfaces
{
    public interface IInputHandler
    {
        string[] SplitInput(string input);
        decimal ConvertAmount(string amount);
    }
}
