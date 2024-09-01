using AdministrationAPI.DTO;

namespace AdministrationAPI.Interfaces
{
    public interface IFeeService
    {
        Task<List<Fee>> GetAllFeesAsync();
    }
}
