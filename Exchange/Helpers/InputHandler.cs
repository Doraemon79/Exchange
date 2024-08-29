using Exchange.Exceptions;
using Exchange.Helpers.Interfaces;

namespace Exchange.Helpers
{
    public class InputHandler : IInputHandler
    {
        public string[] SplitInput(string input)
        {

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new CustomException("Input is empty");
            }
            if (!input.Contains(' ') || !input.Contains('/'))
            {
                throw new CustomException("Input is not written in a meaningful way.");
            }
            char[] delimiters = { '/', ' ' };
            return input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

        }

        public decimal ConvertAmount(string amount)
        {
            decimal result = 0;
            try
            {
                result = Decimal.Parse(amount);
            }
            catch (FormatException)
            {
                Console.WriteLine("Input string is not a valid decimal.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Input string represents a number that is too large or too small.");
            }

            return result;
        }
    }
}
