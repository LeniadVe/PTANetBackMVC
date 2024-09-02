using AdministrationAPI.DTO;
using AdministrationAPI.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AdministrationAPI.Services
{
    public class FeeService : IFeeService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FeeService> _logger;
        private readonly string _url ;

        public FeeService(HttpClient httpClient, ILogger<FeeService> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _url = configuration.GetValue<string>("FeesUrl");
        }
        /// <summary>
        /// Asynchronously retrieves a list of fees from an external API.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Fee>> GetAllFeesAsync()
        {
            _logger.LogInformation("Starting GetAllFeesAsync.");
            try
            {
                return await RetrieveFeeList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching fees from the API.");
                return new List<Fee>();
            }
            finally
            {
                _logger.LogInformation("Finished GetAllFeesAsync.");
            }
        }

        /// <summary>
        /// Makes an asynchronous HTTP GET request to retrieve a list of fees from an external API endpoint.
        /// </summary>    
        /// <returns></returns>
        private async Task<List<Fee>> RetrieveFeeList()
        {
            var fees = new List<Fee>();

            var response = await _httpClient.GetAsync(_url);

            if (!response.IsSuccessStatusCode)
                _logger.LogError($"Failed to retrieve fees. Status Code: {response.StatusCode}");
            else
                fees = DeserializeFees(await response.Content.ReadAsStringAsync());

            if (!fees.Any())
                _logger.LogWarning("The list of items is empty.");

            return fees;
        }
        /// <summary>
        /// Deserializes the JSON response into a list of fees.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private List<Fee> DeserializeFees(string content)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Fee>>(content) ?? new List<Fee>();
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error deserializing the fee response.");
                return new List<Fee>();
            }
        }
    }
}
