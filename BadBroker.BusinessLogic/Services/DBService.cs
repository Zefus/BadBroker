using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BadBroker.BusinessLogic.Interfaces;
using BadBroker.DataAccess.Models;
using BadBroker.DataAccess;

namespace BadBroker.BusinessLogic.Services
{
    public class DBService : IDBService
    {
        public async Task<IEnumerable<TEntity>> GetQuotes<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            try
            {
                using (BadBrokerContext db = new BadBrokerContext())
                {
                    IQueryable<TEntity> query = db.Set<TEntity>().Where(predicate);
                    return await query.ToListAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                throw new NotImplementedException();
            }
        }

        public async Task<IEnumerable<TResult>> SelectQuotes<TEntity, TResult>(Expression<Func<TEntity, TResult>> selector)
            where TEntity : class
        {
            try
            {
                using (BadBrokerContext db = new BadBrokerContext())
                {
                    IQueryable<TResult> query = db.Set<TEntity>().Select(selector);
                    return await query.ToListAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                throw new NotImplementedException();
            }
        }

        public async void AddQuotesRange<TEntity>(IEnumerable<TEntity> entities) 
            where TEntity : class
        {
            try
            {
                using (BadBrokerContext db = new BadBrokerContext())
                {
                    await db.AddRangeAsync(entities);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw new NotImplementedException();
            }
        }
    }
}
