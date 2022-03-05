using FunctionQuery.Enums;
using FunctionQuery.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FunctionQuery.Data.Repositories
{
    public interface IEventRepository
    {
        Task<EventModel> GetLastAsync();
        Task<IEnumerable<EventModel>> GetByTimeintervalAsync(TimeInterval timeInterval);
    }
}
