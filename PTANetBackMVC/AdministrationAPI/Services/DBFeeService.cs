using AdministrationAPI.Context;
using AdministrationAPI.DTO;
using AdministrationAPI.Errors;
using AdministrationAPI.Interfaces;

namespace AdministrationAPI.Services
{
    /// <summary>
    /// Database control service
    /// </summary>
    public class DBFeeService : IDBFeeService
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<DBFeeService> _logger;
        public DBFeeService(DataContext dataContext, ILogger<DBFeeService> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        /// <summary>
        /// Get Fee by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Result<Fee> with Success or Failure response</returns>
        public Result<Fee> Get(int id)
        {
            return ResultHelper.Execute(() =>
            {
                var fee = _dataContext.Fees.FirstOrDefault(x => x.FeeId == id);
                if (fee == null)
                    return Result<Fee>.Failure("Fee not found.");

                _logger.LogInformation($"Get fee {id} successfully.");
                return Result<Fee>.Success(fee);
            }, _logger, "Error retrieving fee");
        }

        /// <summary>
        /// Get all fees
        /// </summary>
        /// <returns>Result<List<Fee>> with Success or Failure response</returns>
        public Result<List<Fee>> GetAll()
        {
            return ResultHelper.Execute(() =>
            {
                var fees = _dataContext.Fees.ToList();
                if (fees == null || !fees.Any())
                    return Result<List<Fee>>.Failure("Fees not found.");

                _logger.LogInformation("Get all fees successfully.");
                return Result<List<Fee>>.Success(fees);
            }, _logger, "Error retrieving fees");
        }

        /// <summary>
        /// Insert fee
        /// </summary>
        /// <returns>Result<List<Fee>> with Success or Failure response</returns>
        public Result<bool> Insert(Fee fee)
        {
            return ResultHelper.Execute(() =>
            {
                _dataContext.Fees.Add(fee);
                _dataContext.SaveChanges();
                _logger.LogInformation("Fee inserted successfully.");
                return Result<bool>.Success(true);
            }, _logger, "Error inserting fee");
        }


        /// <summary>
        /// Insert a range of Fees
        /// </summary>
        /// <returns>
        ///     Result<bool> with Success or Failure response
        ///     True for successfully responde or False for Failure response
        /// </returns>
        public Result<bool> InsertRange(List<Fee> fees)
        {
            return ResultHelper.Execute(() =>
            {
                _dataContext.Fees.AddRange(fees);
                _dataContext.SaveChanges();
                _logger.LogInformation("Fees inserted successfully.");
                return Result<bool>.Success(true);
            }, _logger, "Error inserting fees");
        }

        /// <summary>
        /// Update a Fee
        /// </summary>
        /// <returns>
        ///     Result<bool> with Success or Failure response
        ///     True for successfully responde or False for Failure response
        /// </returns>
        public Result<bool> Update(Fee newFee)
        {
            return ResultHelper.Execute(() =>
            {
                var fee = _dataContext.Fees.FirstOrDefault(x => x.FeeId == newFee.FeeId);
                if (fee == null)
                    return Result<bool>.Failure("Fee not found.");

                _dataContext.Update(newFee);
                _dataContext.SaveChanges();
                _logger.LogInformation("Fee updated successfully.");
                return Result<bool>.Success(true);
            }, _logger, "Error updating fee");
        }

        /// <summary>
        /// Delete Fee by id
        /// </summary>
        /// <returns>
        ///     Result<bool> with Success or Failure response
        ///     True for successfully responde or False for Failure response
        /// </returns>
        public Result<bool> Delete(int id)
        {
            return ResultHelper.Execute(() =>
            {
                var fee = _dataContext.Fees.FirstOrDefault(x => x.FeeId == id);
                if (fee == null)
                    return Result<bool>.Failure("Fee not found.");

                _dataContext.Remove(fee);
                _dataContext.SaveChanges();
                _logger.LogInformation("Fee deleted successfully.");
                return Result<bool>.Success(true);
            }, _logger, "Error deleting fee");
        }

        /// <summary>
        /// Delete a Fee
        /// </summary>
        /// <returns>
        ///     Result<bool> with Success or Failure response
        ///     True for successfully responde or False for Failure response
        /// </returns>
        public Result<bool> Delete(Fee oldFee) => Delete(oldFee.FeeId);

        /// <summary>
        /// Delete all Fees
        /// </summary>
        /// <returns>
        ///     Result<bool> with Success or Failure response
        ///     True for successfully responde or False for Failure response
        /// </returns>
        public Result<bool> DeleteAll()
        {
            return ResultHelper.Execute(() =>
            {
                var fees = _dataContext.Fees.ToList();
                _dataContext.RemoveRange(fees);
                _dataContext.SaveChanges();
                _logger.LogInformation("Fees deleted successfully.");
                return Result<bool>.Success(true);
            }, _logger, "Error deleting fees");
        }
    }
}
