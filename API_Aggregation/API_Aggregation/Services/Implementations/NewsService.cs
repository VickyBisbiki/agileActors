
using API_Aggregation.Services.Interfaces;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace API_Aggregation.Services.Implementations
{
    
    public class NewsService : INewsService
    {

        IHttpClientFactory _httpClientFactory;
        
        private readonly string _newsApiKey;
        private readonly string _baseUrl;

    public NewsService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
            _httpClientFactory = httpClientFactory;

        
            _newsApiKey = configuration["News:ApiKey"];  
            _baseUrl = configuration["News:BaseUrl"];

            if (string.IsNullOrEmpty(_newsApiKey))
            {
                throw new InvalidOperationException("GitHub API token is missing from configuration.");
            }
    }

    public async Task<NewsApiResponse> GetTopHeadlinesNewsAsync(string dateFrom, string sortBy)
    {
          
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_baseUrl);
            client.DefaultRequestHeaders.Add("User-Agent", "API_Aggregation");

        //2024-12-20
        //popularity

        try
        {


            var url = $"everything?q=Apple&from={dateFrom}&sortBy={sortBy}&apiKey={_newsApiKey}";
            var response = await client.GetAsync(Uri.EscapeUriString(url));
           

            if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Unexpected status code: {response.StatusCode}");

            var data = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<NewsApiResponse>(data);

            return result;

        }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching news from the API.", ex);
            }
        }

      
    }
        

}
