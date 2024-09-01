using AdministrationAPI.DTO;
using AdministrationAPI.Interfaces;
using Newtonsoft.Json;

namespace AdministrationAPI.Services
{
    public class FeeService : IFeeService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FeeService> _logger;
        private readonly string _url = "https://api.opendata.esett.com/EXP05/Fees";

        public FeeService(HttpClient httpClient, ILogger<FeeService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<Fee>> GetAllFeesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_url);

                if (response == null || !response.IsSuccessStatusCode)
                {
                    _logger.LogError("No response from GetAllFees.");
                    return new List<Fee>();
                }

                var fees = JsonConvert.DeserializeObject<List<Fee>>(await response.Content.ReadAsStringAsync()) ?? new List<Fee>();
                if (!fees.Any())
                {
                    _logger.LogWarning("The list of items is empty.");
                }
                return fees;
            }
            catch (Exception)
            {
                _logger.LogError("Error getting GetAllFees response.");
                return new List<Fee>();
            }
        }
    }
}
