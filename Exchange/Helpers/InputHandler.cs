using Exchange.Exceptions;
using Exchange.Helpers.Interfaces;
using Exchange.Models;

namespace Exchange.Helpers
{
    public class InputHandler : IInputHandler
    {
        public List<ExchangeRequest> SplitInput(string input)
        {
            List<ExchangeRequest> exchangeRequests = [];
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new CustomException("Input is empty");
            }
            if (!input.Contains(' ') || !input.Contains('/'))
            {
                throw new CustomException("Input is not written in a meaningful way.");
            }
            var requests = input.Split(';').ToList();

            foreach (var el in requests)
            {
                char[] delimiters = { '/', ' ' };
                string[] singleRequest = el.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                ExchangeRequest exchangeRequest = new ExchangeRequest(singleRequest[1], singleRequest[2], ConvertAmount(singleRequest[0]));
                exchangeRequests.Add(exchangeRequest);
            }
            return exchangeRequests;
        }

        public decimal ConvertAmount(string amount)
        {
            decimal result = 0;
            try
            {
                result = Decimal.Parse(amount.Replace(',', '.'));
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
