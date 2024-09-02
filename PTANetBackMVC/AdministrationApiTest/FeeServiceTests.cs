using AdministrationAPI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace AdministrationApiTest
{
    public class FeeServiceTests
    {
        private readonly Mock<HttpClient> _mockHttpClient;
        private readonly Mock<ILogger<FeeService>> _mockLogger;
        private readonly FeeService _feeService;

        public FeeServiceTests()
        {
            _mockHttpClient = new Mock<HttpClient>();
            _mockLogger = new Mock<ILogger<FeeService>>();

            var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var config = configurationBuilder.Build();

            _feeService = new FeeService(_mockHttpClient.Object, _mockLogger.Object, config);
        }

        [Fact]
        public async Task GetAllFeesAsync_ReturnsListOfFees()
        {
            // Arrange
            var feesCount = 58;

            // Act
            var result = await _feeService.GetAllFeesAsync();

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(feesCount, result.Count);
        }
    }
}