using CarDealer.Application.ExternalContracts;
using System;
using System.Net.Http;

namespace CarDealer.Infrastructure.Sale
{
    internal class ExternalSystemClient : IExternalSystemClient
    {
        private readonly HttpClient _client;

        public ExternalSystemClient(HttpClient httpClient)
        {
            _client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            httpClient.BaseAddress = new Uri("https://some.external.api.com/");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            _client = httpClient;
        }

        public int GetMileage(string vin)
        {
            //return await _client.GetStringAsync("/");
            return 80_000;
        }
    }
}
