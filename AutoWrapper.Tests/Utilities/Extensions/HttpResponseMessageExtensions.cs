using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using FluentAssertions;

namespace AutoWrapper.WebAPI.Tests.Utilities.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static void ShouldReturnSuccessStatusCode(this HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }

        public static void ShouldHaveValidJsonContentType(this HttpResponseMessage response)
        {
            if (response.Content.Headers.ContentLength == 0)
                throw new InvalidDataException("no content type in response found");

            var contentType = response.Content.Headers.ContentType.ToString();
            contentType.Should().Be("application/json");
        }

        public static async Task<T> ReadContentAsJsonAsync<T>(this HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            var responseContent = content;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            
            var contentJson = JsonSerializer.Deserialize<T>(responseContent, options);
            return contentJson;
        }
    }
}
