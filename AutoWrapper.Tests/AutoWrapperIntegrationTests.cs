using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using FluentAssertions;
using AutoWrapper.WebAPI.Tests.Utilities.Extensions;
using AutoWrapper.WebAPI.Tests.Utilities;

namespace AutoWrapper.WebAPI.Tests
{
    [TestClass]
    public class AutoWrapperIntegrationTests
    {
		public const string CreateWeatherForecastEndpoint = "api/WeatherForecast";
        private readonly SimpleWebApplicationFactory factory;

        public AutoWrapperIntegrationTests()
		{
            this.factory = new SimpleWebApplicationFactory();
        }

		[TestMethod]
        public async Task TestPost()
        {
			// Arrange
			using var client = CreateClient();
			var wfc = new WeatherForecast
			{
				Date = DateTime.Now,
				TemperatureC = 30
			};

			// Act
			var jsonContent = new JsonContent(wfc);
			var responseMessage = await client.PostAsync(CreateWeatherForecastEndpoint, jsonContent);

			// Assert
			responseMessage.ShouldReturnSuccessStatusCode();
			responseMessage.ShouldHaveValidJsonContentType();

			var response = await responseMessage.ReadContentAsJsonAsync<WeatherForecast>();

			response.Summary.Should().NotBeNullOrEmpty();
			jsonContent.Dispose();
		}

		protected HttpClient CreateClient() => factory.CreateClient();
	}
}
