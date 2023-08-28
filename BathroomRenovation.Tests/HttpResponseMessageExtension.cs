
using System.Text.Json;

namespace BathroomRenovation.Tests
{
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Deserialize HttpResponse using System.Text.Json with sane default options
        /// </summary>
        public static async Task<T> DeserializeBody<T>(
            this HttpResponseMessage response,
            Action<JsonSerializerOptions>? customOptions = null)
        {
            var body = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            customOptions?.Invoke(options);

            return JsonSerializer.Deserialize<T>(body, options)!;
        }
    }
}
