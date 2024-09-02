using AdministrationAPI.DTO;
using AdministrationAPI.Errors;
using AdministrationAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdministrationAPI.Controllers
{
    public class FeesController : ControllerBase
    {
        private readonly IFeeService _feeService;
        private readonly IDBFeeService _dbFeeService;
        private readonly ILogger<FeesController> _logger;

        public FeesController(IFeeService feeService, IDBFeeService dbFeeService, ILogger<FeesController> logger)
        {
            _feeService = feeService;
            _dbFeeService = dbFeeService;
            _logger = logger;
        }

        [HttpGet("GetAllFees")]
        public async Task<IActionResult> GetAllFees()
        {
            _logger.LogInformation($"GetAllFees was successfully called.");
            var fees = await _feeService.GetAllFeesAsync();
            var result = _dbFeeService.InsertRange(fees);
            return HandleResult(result, StatusCodes.Status400BadRequest, "An error occurred while retrieving the fees.");
        }

        [HttpGet("")]
        public IActionResult GetFees()
        {
            _logger.LogInformation($"GetFees was successfully called.");

            var fees = _dbFeeService.GetAll();
            return HandleResult(fees, StatusCodes.Status404NotFound, "An error occurred while retrieving the fee.");
        }

        [HttpGet("{id}")]
        public IActionResult GetFee(int id)
        {
            _logger.LogInformation($"GetFee with Id:{id} was successfully called.");

            var fee = _dbFeeService.Get(id);
            return HandleResult(fee, StatusCodes.Status404NotFound, "An error occurred while retrieving the fee.");
        }

        [HttpPost("")]
        public IActionResult InsertFee(Fee fee)
        {
            _logger.LogInformation($"InsertFee was successfully called.");

            var result = _dbFeeService.Insert(fee);
            return HandleResult(result, StatusCodes.Status400BadRequest, "An error occurred while inserting the fee.");
        }

        [HttpPut("{id}")]
        public IActionResult PutMarketData(int id, Fee fee)
        {
            _logger.LogInformation($"PutMarketData with Id:{id} was successfully called.");

            if (id != fee.FeeId)
                return BadRequest();

            var result = _dbFeeService.Update(fee);
            return HandleResult(result, StatusCodes.Status400BadRequest, $"An error occurred while updating the fee.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFee(int id)
        {
            _logger.LogInformation($"DeleteFee with Id:{id} was successfully called.");

            var result = _dbFeeService.Delete(id);
            return HandleResult(result, StatusCodes.Status400BadRequest, "An error occurred while updating the fee.");
        }

        private IActionResult HandleResult<T>(Result<T> result,int statusCode, string message)
        {
            return ResultHelper.HandleResult(result, _logger, Ok, StatusCode, statusCode, message);
        }
    }

}
