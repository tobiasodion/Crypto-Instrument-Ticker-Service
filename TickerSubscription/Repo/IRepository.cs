using System.Threading.Tasks;

namespace TickerSubscription.Repo;

public interface IRepository<T> where T : class
{
    Task AddAsync(T entity);
}