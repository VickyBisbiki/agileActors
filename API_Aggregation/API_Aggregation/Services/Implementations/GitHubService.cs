
using API_Aggregation.Models;
using Newtonsoft.Json;
using API_Aggregation.Services.Interfaces;


namespace API_Aggregation.Services.Implementations
{

    public class GitHubService : IGitHubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _githubApiToken;
        private readonly string _baseUrl;

        public GitHubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            
            _githubApiToken = configuration["GitHub:BearerToken"];  
            _baseUrl = configuration["GitHub:BaseUrl"];  

            if (string.IsNullOrEmpty(_githubApiToken))
            {
                throw new InvalidOperationException("GitHub API token is missing from configuration.");
            }  

        }

        public async Task<GitHubApiResponse> GetUserAsync(string userName)
        {
            var client = _httpClientFactory.CreateClient();

         
            client.BaseAddress = new Uri(_baseUrl);
   
            client.DefaultRequestHeaders.Add("User-Agent", "C# App");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _githubApiToken);

            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("User name cannot be null or empty.", nameof(userName));

            try
            {
                var response = await client.GetAsync($"users/{userName}"); 

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Unexpected status code: {response.StatusCode}");

                var data = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<GitHubApiResponse>(data);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching data from GitHub.", ex);
            }
        }
    }

}
