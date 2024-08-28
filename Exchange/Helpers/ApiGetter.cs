using Exchange.Helpers.Interfaces;
using Serilog;
using System.Collections.Frozen;
using System.Text.Json;

namespace Exchange.Helpers
{
    public class ApiGetter : IApiGetter
    {

        private static readonly string apiKey = "fca_live_rwJH4eFRefty50fCgZGcRqSpm2vUHOkT2mkgDTxr";

        private static readonly string baseUrl = "https://api.freecurrencyapi.com/v1/latest";

        /// <summary>
        /// Fetches the latest currency exchange rates from FreecurrencyAPI.
        /// </summary>
        public async Task<FrozenDictionary<string, decimal>> GetRates()
        {
            using HttpClient client = new HttpClient();

            // Build the request URL
            string requestUrl = $"{baseUrl}?apikey={apiKey}";

            try
            {
                Log.Information($"Fetching currency data from {requestUrl}");
                // Send the GET request to the API
                HttpResponseMessage response = await client.GetAsync(requestUrl);

                // Ensure the response is successful
                response.EnsureSuccessStatusCode();

                // Read the JSON response as a string
                string jsonResponse = await response.Content.ReadAsStringAsync();

                // Step 1: Deserialize the JSON string into a Dictionary<string, decimal>
                var currencyResponse = JsonSerializer.Deserialize<CurrencyResponse>(jsonResponse);

                // Step 2: Convert the Dictionary to a FrozenDictionary
                FrozenDictionary<string, decimal> frozenRates = currencyResponse?.data?.ToFrozenDictionary();


                // Check if the dictionary contains the currency data
                if (frozenRates != null)
                {
                    return frozenRates;
                }
                else
                {
                    throw new Exception("Failed to fetch currency data.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return FrozenDictionary<string, decimal>.Empty;
            }
        }
    }
}

// Define a class to hold the response data
public class CurrencyResponse
{
    public Dictionary<string, decimal> data { get; set; }
}