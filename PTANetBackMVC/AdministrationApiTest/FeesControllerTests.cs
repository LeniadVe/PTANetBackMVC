using AdministrationAPI.Controllers;
using AdministrationAPI.DTO;
using AdministrationAPI.Errors;
using AdministrationAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace AdministrationApiTest
{
    public  class FeesControllerTests
    {
        private readonly Mock<IFeeService> _mockFeeService;
        private readonly Mock<IDBFeeService> _mockDbFeeService;
        private readonly Mock<ILogger<FeesController>> _mockLogger;
        private readonly FeesController _controller;
        public FeesControllerTests()
        {
            _mockFeeService = new Mock<IFeeService>();
            _mockDbFeeService = new Mock<IDBFeeService>();
            _mockLogger = new Mock<ILogger<FeesController>>();

            _controller = new FeesController(_mockFeeService.Object, _mockDbFeeService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllFees_ReturnsOkResultWithFees()
        {
            // Arrange
            var fees = new List<Fee> { new Fee { FeeId = 1, Country = "Country1" } };
            _mockFeeService.Setup(service => service.GetAllFeesAsync()).ReturnsAsync(fees);
            _mockDbFeeService.Setup(service => service.InsertRange(fees)).Returns(Result<bool>.Success(true));

            // Act
            var result = await _controller.GetAllFees();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var resultValue = Assert.IsType<List<Fee>>(actionResult.Value);
            Assert.Contains(fees.FirstOrDefault(),resultValue);
        }

        [Fact]
        public async Task GetAllFees_ReturnsBadRequestOnFailure()
        {
            // Arrange
            _mockFeeService.Setup(service => service.GetAllFeesAsync()).ReturnsAsync(new List<Fee>());
            _mockDbFeeService.Setup(service => service.InsertRange(It.IsAny<List<Fee>>())).Returns(Result<bool>.Failure("Error"));

            // Act
            var result = await _controller.GetAllFees();

            // Assert
            Assert.Null(result);
        }
    }
}
