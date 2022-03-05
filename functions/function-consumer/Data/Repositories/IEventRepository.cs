using FunctionConsumer.Models;
using System.Threading.Tasks;

namespace FunctionConsumer.Data.Repositories
{
    public interface IEventRepository
    {
        Task AddAsync(EventModel @event);
    }
}
