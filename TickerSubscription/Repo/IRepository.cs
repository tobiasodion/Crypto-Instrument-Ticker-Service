using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TickerSubscription.Repo;

public interface IRepository<T> where T : class
{
    Task AddAsync(T entity);

    Task<IReadOnlyCollection<T>> GetAllForPeriodAsync(DateTime startTimestamp, DateTime endTimestamp);
}