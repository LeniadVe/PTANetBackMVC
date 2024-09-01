using AdministrationAPI.Context;
using AdministrationAPI.DTO;
using AdministrationAPI.Interfaces;
using AdministrationAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdministrationAPI.Controllers
{
    public class FeesController : ControllerBase
    {
        private readonly IFeeService _feeService;
        private readonly DataContext _dbContext;

        public FeesController(IFeeService feeService, DataContext dbContext)
        {
            _feeService = feeService;
            _dbContext = dbContext;
        }

        [HttpGet("GetAllFees")]
        public async Task<IActionResult> GetAllFees()
        {
            var fees = await _feeService.GetAllFeesAsync();
            _dbContext.Fees.AddRange(fees);
            await _dbContext.SaveChangesAsync();
            if (fees == null || !fees.Any())
                return NotFound();
            return Ok(fees);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetFees()
        {
            var fees = _dbContext.Fees.ToListAsync();
            if (fees == null || fees.IsFaulted || !fees.Result.Any())
                return NotFound();
            return Ok(await fees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFee(int id)
        {
            var fee = _dbContext.Fees.FirstOrDefaultAsync(x => x.FeeId == id);
            if (fee == null || fee.IsFaulted)
                return NotFound();
            return Ok(await fee);
        }

        [HttpPost]
        public async Task<IActionResult> InsertFee(Fee fee)
        {
            try
            {
                _dbContext.Fees.Add(fee);
                await _dbContext.SaveChangesAsync();

                return Created("GetFee", new { id = fee.FeeId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMarketData(int id, Fee fee)
        {
            if (id != fee.FeeId)
                return BadRequest();

            _dbContext.Entry(fee).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeeExists(id))
                    return NotFound();
                else
                    return BadRequest();
            }

            return Ok();
        }
        private bool FeeExists(int id) => _dbContext.Fees.Any(x => x.FeeId == id);

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFee(int id)
        {
            var fee = await _dbContext.Fees.FindAsync(id);
            if (fee == null)
                return NotFound();
            try
            {
                _dbContext.Fees.Remove(fee);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

}
