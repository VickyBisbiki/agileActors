using System;
using System.Globalization;
using System.Net.Http;
using API_Aggregation.Models;
using API_Aggregation.Services;
using API_Aggregation.Services.Interfaces;
using Newtonsoft.Json;


namespace API_Aggregation.Services.Implementations
{
    
    public class WeatherService : IWeatherService
    {
        IHttpClientFactory _httpClientFactory;

        private readonly string _weatherApiKey;
        private readonly string _baseUrl;


        public WeatherService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _weatherApiKey = configuration["Weather:ApiKey"];
            _baseUrl = configuration["Weather:BaseUrl"];

            if (string.IsNullOrEmpty(_weatherApiKey))
            {
                throw new InvalidOperationException("GitHub API token is missing from configuration.");
            }

        }

        public async Task<WeatherApiResponse> GetWeatherAsync(string city)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_baseUrl);

            client.DefaultRequestHeaders.Add("User-Agent", "API_Aggregation");

            try
            {
                var url = $"weather?q={city}&appid={_weatherApiKey}";
                var response = await client.GetAsync(Uri.EscapeUriString(url));


                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Unexpected status code: {response.StatusCode}");

                var data = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<WeatherApiResponse>(data);

                return result;

            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching news from the API.", ex);
            }
        }
          
           
        }
    }

    
    