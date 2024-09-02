using AdministrationAPI.DTO;
using AdministrationAPI.Errors;

namespace AdministrationAPI.Interfaces
{
    public interface IDBFeeService
    {
        Result<Fee> Get(int id);

        Result<List<Fee>> GetAll();

        Result<bool> Insert(Fee fee);

        Result<bool> InsertRange(List<Fee> fees);

        Result<bool> Update(Fee fee);

        Result<bool> Delete(int id);
        Result<bool> Delete(Fee fee);

        Result<bool> DeleteAll();
    }
}
