using System.Text;
using Newtonsoft.Json;

namespace GustoUIConsole.Services
{
    public abstract class ApiServiceBase
    {
        protected static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:52416")
        };

        protected StringContent CreateJsonContent<T>(T data)
        {
            return new StringContent(
                JsonConvert.SerializeObject(data, Formatting.None),
                Encoding.UTF8,
                "application/json");
        }

        public static void SetApiKey(string apiKey)
        {
            _httpClient.DefaultRequestHeaders.Remove("X-API-KEY");

            if (!string.IsNullOrEmpty(apiKey))
            {
                _httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey);
            }
        }

        protected async Task HandleResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                string formattedString = JsonConvert.SerializeObject(content, Formatting.Indented);

                Console.WriteLine($"Success: {formattedString}");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error ({response.StatusCode}): {errorContent}");
            }
        }
        protected async Task<T> HandleResponse<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error ({(int)response.StatusCode}): {content}");
                return default;
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Deserialization error: {ex.Message}");
                return default;
            }
        }
    }
}
