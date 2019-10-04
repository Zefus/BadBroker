using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BadBroker.DataAccess.Models;

namespace BadBroker.BusinessLogic.Interfaces
{
    public interface IDBService
    {
        Task<IEnumerable<TEntity>> GetQuotes<TEntity>(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken,
            params Expression<Func<TEntity, object>>[] includes) where TEntity : class;

        Task<IEnumerable<TResult>> SelectQuotes<TEntity, TResult>(Expression<Func<TEntity, TResult>> selector) 
            where TEntity : class;

        Task AddQuotesRange<TEntity>(IEnumerable<TEntity> entity) 
            where TEntity : class;
    }
}
