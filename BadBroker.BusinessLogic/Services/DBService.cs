using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BadBroker.BusinessLogic.Interfaces;
using BadBroker.DataAccess;
using BadBroker.BusinessLogic.Exceptions;
using BadBroker.DataAccess.Models;

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
            catch (SqlException ex)
            {
                throw new DBServiceException(ex.Message, ex);
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
            catch (SqlException ex)
            {
                throw new DBServiceException(ex.Message, ex);
            }
        }

        public Task AddQuotesRange<TEntity>(IEnumerable<TEntity> entities) 
            where TEntity : class
        {
            try
            {
                using (BadBrokerContext db = new BadBrokerContext())
                {
                    db.AddRange(entities);
                    db.SaveChanges();
                    return Task.FromResult(1);
                }
            }
            catch(SqlException ex)
            {
                throw new DBServiceException(ex.Message, ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    if (entry.Entity is QuotesData)
                    {
                        var proposedValues = entry.CurrentValues;
                        var databaseValues = entry.GetDatabaseValues();

                        foreach (var property in proposedValues.Properties)
                        {
                            var proposedValue = proposedValues[property];
                            var databaseValue = databaseValues[property];

                            proposedValues[property] = databaseValue;
                        }

                        entry.OriginalValues.SetValues(databaseValues);
                    }
                    else
                    {
                        throw new NotSupportedException(
                            "Don't know how to handle concurrency conflicts for "
                            + entry.Metadata.Name);
                    }
                }
                return Task.FromResult(1);
            }
            catch (DbUpdateException ex)
            {
                throw new DBServiceException(ex.Message, ex);
            }
        }
    }
}
