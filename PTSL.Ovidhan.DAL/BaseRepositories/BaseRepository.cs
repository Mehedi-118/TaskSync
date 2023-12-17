using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using PTSL.Ovidhan.Common.Entity;
using PTSL.Ovidhan.Common.Entity.CommonEntity;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.QuerySerialize.Implementation;
using PTSL.Ovidhan.Common.QuerySerialize.Interfaces;
using PTSL.Ovidhan.DAL.Repositories.Interface;
using PTSL.Ovidhan.Common.Implementation;

using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PTSL.Ovidhan.DAL.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly Microsoft.EntityFrameworkCore.DbContext WriteOnlyCtx;
        private readonly Microsoft.EntityFrameworkCore.DbContext ReadOnlyCtx;

        private readonly DbSet<T> WriteOnlySet;
        private readonly DbSet<T> ReadOnlySet;
        public BaseRepository(Microsoft.EntityFrameworkCore.DbContext writeOnlyCtx, Microsoft.EntityFrameworkCore.DbContext readOnlyCtx)
        {
            this.WriteOnlyCtx = writeOnlyCtx;
            this.ReadOnlyCtx = readOnlyCtx;
            this.WriteOnlySet = this.WriteOnlyCtx.Set<T>();
            this.ReadOnlySet = this.ReadOnlyCtx.Set<T>();
        }
        public virtual async Task<bool> SaveAsync(IDbContextTransaction transaction)
        {
            if (transaction != null)
            {
                if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
                {
                    try
                    {
                        await WriteOnlyCtx.SaveChangesAsync();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public virtual async Task<(ExecutionState executionState, T entity, string message)> CreateAsync(T entity)
        {

            if (entity != null)
            {
                try
                {
                    await WriteOnlySet.AddAsync(entity);

                    return (ExecutionState.Created, entity, $"New {typeof(T).Name} item added.");
                }
                catch (Exception ex)
                {
                    return (executionState: ExecutionState.Failure, entity: null, message: ex.Message);
                }
            }
            else
            {
                return (executionState: ExecutionState.Failure, entity: null, message: null);
            }

        }

        public async Task<(ExecutionState executionState, IList<T> entity, string message)> CreateAsync(IList<T> entity)
        {
            if (entity != null)
            {
                try
                {
                    await WriteOnlySet.AddRangeAsync(entity);

                    return (ExecutionState.Created, entity, $"New {typeof(T).Name} item added.");
                }
                catch (Exception ex)
                {
                    return (executionState: ExecutionState.Failure, entity: null, message: ex.Message);
                }
            }
            else
            {
                return (executionState: ExecutionState.Failure, entity: null, message: null);
            }
        }

        public virtual async Task<(ExecutionState executionState, T entity, string message)> GetAsync(
            long id,
            RetrievalPurpose retrievalPurpose = RetrievalPurpose.Consumption)
        {
            T entity = null;

            // TODO: after implementing ValidateKey as common validation method, we will first call it here to make sure the Key is valid

            try
            {
                entity = (retrievalPurpose == RetrievalPurpose.Manipulation)
                    ? await WriteOnlySet.IgnoreQueryFilters()
                        .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.IsDeleted != true)
                    //.FirstOrDefaultAsync(x => x.Id.Equals(id) && x.IsDeleted != 1)
                    : await ReadOnlySet
                        .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.IsDeleted != true);
                //.FirstOrDefaultAsync(x => x.Id.Equals(id) && x.IsDeleted != 1);
            }
            catch (Exception ex)
            {
                return (executionState: ExecutionState.Failure, entity: null, message: ex.Message);
            }

            return (entity != null)
                ? (executionState: ExecutionState.Retrieved, entity: entity, $"{typeof(T).Name} item found.")
                : (executionState: ExecutionState.Failure, entity: null, $"{typeof(T).Name} item not found.");
        }

        public virtual async Task<(ExecutionState executionState, T entity, string message)> GetAsync(
            IFilterOptions<T> filterOptions,
            RetrievalPurpose retrievalPurpose = RetrievalPurpose.Consumption)
        {
            (ExecutionState executionState, T entity, string message) getResponse;
            T entity = null;

            if (filterOptions != null)
            {
                IQueryable<T> query = null;

                if (filterOptions.ListCondition == Common.Enum.List.IncludeInactives)
                {
                    query = (retrievalPurpose == RetrievalPurpose.Manipulation) ?
                        WriteOnlySet
                            .IgnoreQueryFilters() :
                        ReadOnlySet
                            .IgnoreQueryFilters();

                    query = query.Where(x => !x.IsDeleted);
                    //query = query.Where(x => x.IsDeleted == 0);
                }
                else
                {
                    query = (retrievalPurpose == RetrievalPurpose.Manipulation)
                        ? WriteOnlySet
                        : ReadOnlySet;
                    query = query.Where(x => !x.IsDeleted);
                    //query = query.Where(x => x.IsDeleted == 0);
                }

                Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>> includeExpression = filterOptions.IncludeExpression;
                Expression<Func<T, bool>> filterExpression = filterOptions.FilterExpression;

                try
                {
                    // apply include
                    if (includeExpression != null)
                    {
                        query = (includeExpression.Compile())(query);
                    }

                    // apply filter
                    if (filterExpression != null)
                    {
                        query = query?.AsEnumerable()
                            .Where(filterExpression.Compile())
                            .AsQueryable();
                    }

                    //TODO: create one execution path with async, and another without, for this scenario

                    //T entity = await query.FirstOrDefaultAsync ();
                    if (query != null)
                    {
                        entity = query.FirstOrDefault();
                        //.FirstOrDefaultAsync ();
                    }
                    else
                    {
                        entity = null;
                    }
                }
                catch (Exception ex)
                {
                    getResponse = (executionState: ExecutionState.Failure, entity: null, message: ex.Message);
                }
            }
            else
            {
                getResponse = (executionState: ExecutionState.Failure, entity: null, message: "Filter Options not inserted.");
            }

            getResponse = (entity != null)
                ? (executionState: ExecutionState.Retrieved, entity: entity, $"{typeof(T).Name} item found.")
                : (executionState: ExecutionState.Failure, entity: null, $"{typeof(T).Name} item not found.");

            return getResponse;
        }

        public virtual async Task<(ExecutionState executionState, IQueryable<T> entity, string message)> List(IQueryOptions<T> queryOptions = null)
        {
            (ExecutionState executionState, IQueryable<T> entity, string message) listResponse;
            IQueryable<T> query = null;
            IQueryable<T> entities = null;

            if (queryOptions != null && queryOptions.ListCondition == Common.Enum.List.IncludeInactives)
            {
                query = ReadOnlySet.IgnoreQueryFilters();

                query = query.Where(x => !x.IsDeleted);
                //query = query.Where(x => x.IsDeleted == 0);
            }
            else
            {
                query = ReadOnlySet.Where(x => !x.IsDeleted);
                //query = ReadOnlySet.Where(x=> x.IsDeleted == 0);
            }

            if (queryOptions != null)
            {
                Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>> includeExpression = queryOptions.IncludeExpression;
                Expression<Func<T, bool>> filterExpression = queryOptions.FilterExpression;
                Expression<Func<IQueryable<T>, IOrderedQueryable<T>>> sortExpression = queryOptions.SortingExpression;
                Pagination pagination = queryOptions.Pagination;

                try
                {
                    // apply include
                    if (includeExpression != null)
                    {
                        query = (includeExpression.Compile())(query);
                    }

                    // apply filter
                    if (filterExpression != null)
                    {
                        query = query?.AsEnumerable()
                            .Where(filterExpression.Compile())
                            .AsQueryable();
                    }

                    // apply sorting
                    if (sortExpression != null)
                    {
                        query = (sortExpression.Compile())(query);
                    }

                    // apply pagination
                    if (pagination != null)
                    {
                        query = query?
                            .Skip(pagination.Start)
                                .Take(pagination.Limit);
                    }

                    entities = query?.AsQueryable();
                }
                catch (Exception ex)
                {
                    listResponse = (executionState: ExecutionState.Failure, entity: null, message: ex.Message.ToString());
                }
            }
            else
            {
                entities = query?.AsQueryable();
                //listResponse = (executionState: ExecutionState.Failure, entity: null, message: "Invalid Query Option.");
            }

            listResponse = (entities != null && entities.Any())
                ? (executionState: ExecutionState.Retrieved, entity: entities, $"{typeof(T).Name} item found.")
                : (executionState: ExecutionState.Failure, entity: null, $"{typeof(T).Name} item not found.");

            return listResponse;
        }

        public virtual async Task<(ExecutionState executionState, PagedData<T> data, string message)> Page(IQueryOptions<T> queryOptions = null)
        {
            (ExecutionState executionState, PagedData<T> entity, string message) pageResponse;
            IQueryable<T> query = null;
            IQueryable<T> entities = null;

            if (queryOptions != null && queryOptions.ListCondition == Common.Enum.List.IncludeInactives)
            {
                query = ReadOnlySet.IgnoreQueryFilters();

                query = query.Where(x => !x.IsDeleted);
                //query = query.Where(x => x.IsDeleted == 0);
            }
            else
            {
                query = ReadOnlySet.Where(x => !x.IsDeleted);
                //query = ReadOnlySet.Where(x=> x.IsDeleted == 0);
            }

            decimal totalRecord = 0;

            if (queryOptions != null)
            {
                Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>> includeExpression = queryOptions.IncludeExpression;
                Expression<Func<T, bool>> filterExpression = queryOptions.FilterExpression;
                Expression<Func<IQueryable<T>, IOrderedQueryable<T>>> sortExpression = queryOptions.SortingExpression;
                Pagination pagination = queryOptions.Pagination;

                try
                {
                    // apply include
                    if (includeExpression != null)
                    {
                        query = (includeExpression.Compile())(query);
                    }

                    // apply filter
                    if (filterExpression != null)
                    {
                        query = query?.AsEnumerable()
                            .Where(filterExpression.Compile())
                            .AsQueryable();
                    }

                    // apply sorting
                    if (sortExpression != null)
                    {
                        query = (sortExpression.Compile())(query);
                    }

                    totalRecord = query.Count();

                    // apply pagination
                    if (pagination != null)
                    {
                        query = query?
                            .Skip(pagination.Start)
                                .Take(pagination.Limit);
                    }

                    entities = query?.AsQueryable();
                }
                catch (Exception ex)
                {
                    pageResponse = (executionState: ExecutionState.Failure, entity: null, message: ex.Message.ToString());
                }
            }
            else
            {
                entities = query?.AsQueryable();
                //listResponse = (executionState: ExecutionState.Failure, entity: null, message: "Invalid Query Option.");
            }

            var pageNumber = queryOptions.Pagination.PageNumber;
            var pageLimit = queryOptions.Pagination.Limit;
            var totalPage = Math.Ceiling((decimal)totalRecord / (decimal)pageLimit);

            var pagedData = new PagedData<T>
            {
                PageNumber = pageNumber,
                PageSize = pageLimit,
                TotalPage = totalPage,
                TotalRecord = totalRecord,
                Data = entities.ToList()
            };

            pageResponse = (entities != null)
                ? (executionState: ExecutionState.Retrieved, data: pagedData, $"{typeof(T).Name} item found.")
                : (executionState: ExecutionState.Failure, data: new PagedData<T>(), $"{typeof(T).Name} item not found.");

            return pageResponse;
        }

        public virtual async Task<(ExecutionState executionState, string message)> DoesExistAsync(long key)
        {
            (ExecutionState executionState, string message) getResponse;
            bool doesExist = false;

            // TODO: after implementing ValidateKey as common validation method, we will first call it here to make sure the Key is valid

            try
            {
                doesExist = await ReadOnlySet.AnyAsync(x => x.Id.Equals(key) && x.IsDeleted != true);
                //doesExist = await ReadOnlySet.AnyAsync(x => x.Id.Equals(key) && x.IsDeleted != 1);

                getResponse = (doesExist)
                ? (executionState: ExecutionState.Success, $"{typeof(T).Name} item found.")
                : (executionState: ExecutionState.Failure, $"{typeof(T).Name} item not found.");
            }
            catch (Exception ex)
            {
                getResponse = (executionState: ExecutionState.Failure, message: ex.Message.ToString());
            }


            return getResponse;
        }

        public virtual async Task<(ExecutionState executionState, string message)> DoesExistAsync(IFilterOptions<T> filterOptions)
        {
            (ExecutionState executionState, string message) getResponse;
            bool doesExist = false;

            if (filterOptions != null)
            {
                Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>> includeExpression = filterOptions.IncludeExpression;
                Expression<Func<T, bool>> filterExpression = filterOptions.FilterExpression;

                IQueryable<T> query = ReadOnlySet;
                query = query.Where(x => x.IsDeleted != true);
                //query = query.Where(x => x.IsDeleted != 1);
                try
                {
                    // apply include
                    if (includeExpression != null)
                    {
                        query = (includeExpression.Compile())(query);
                    }

                    // apply filter
                    if (filterExpression != null)
                    {
                        query = query?.AsEnumerable()
                            .Where(filterExpression.Compile())
                            .AsQueryable();
                    }

                    //                    if (query != null && await query.AnyAsync ())
                    if (query != null && query.Any())
                    {
                        doesExist = true;
                    }
                }
                catch (Exception ex)
                {
                    getResponse = (executionState: ExecutionState.Failure, message: ex.Message.ToString());
                }
            }
            else
            {
                getResponse = (executionState: ExecutionState.Failure, message: "Filter Options not inserted.");
            }

            getResponse = (doesExist)
                ? (executionState: ExecutionState.Success, $"{typeof(T).Name} item found.")
                : (executionState: ExecutionState.Failure, $"{typeof(T).Name} item not found.");
            return getResponse;
        }

        public virtual async Task<(ExecutionState executionState, T entity, string message)> UpdateAsync(T entity)
        {
            (ExecutionState executionState, T entity, string message) updateResponse;
            bool success = false;

            // TODO: apply this same mechanism of persisting audit entities in MarkAsActiveAsync and MarkAsInactiveAsync -- not needed ==> VERIFY AND DEBUG IT
            (ExecutionState executionState, T retrievedEntity, string message) = await GetAsync(entity.Id, RetrievalPurpose.Manipulation);

            if (executionState == ExecutionState.Retrieved && retrievedEntity != null)
            {
                try
                {
                    if (entity.PersistAuditProperties<T>(ref retrievedEntity))
                    {
                        //WriteOnlySet.Add(retrievedEntity).State = EntityState.Modified;  //TODO: validate if this is correct to remove
                        //WriteOnlySet.Add(retrievedEntity).State = EntityState.Detached;
                        WriteOnlySet.Update(retrievedEntity);        //TODO: verify if this update is indeed necessary

                        success = true;
                    }

                }
                catch (Exception ex)
                {
                    success = false;

                    updateResponse = (executionState: ExecutionState.Failure, entity: null, message: $"Problem on {typeof(T).Name} update. - {ex.Message.ToString()}");
                }
            }

            updateResponse = (executionState == ExecutionState.Retrieved && retrievedEntity != null && success)
                ? (executionState: ExecutionState.Updated, entity: retrievedEntity, message: "Item updated successfully.")
                : (executionState: ExecutionState.Failure, entity: null, message: $"{typeof(T).Name} item not found.");
            return updateResponse;
        }

        public async Task<(ExecutionState executionState, IList<T> entity, string message)> UpdateAsync(IList<T> entity)
        {
            if (entity != null)
            {
                try
                {
                    WriteOnlySet.UpdateRange(entity);

                    return (ExecutionState.Created, entity, $"New {typeof(T).Name} item added.");
                }
                catch (Exception ex)
                {
                    return (executionState: ExecutionState.Failure, entity: null, message: ex.Message);
                }
            }
            else
            {
                return (executionState: ExecutionState.Failure, entity: null, message: null);
            }
        }

        // Remove from Base Entity is hard delete
        public virtual async Task<(ExecutionState executionState, T entity, string message)> RemoveAsync(long id)
        {
            (ExecutionState executionState, T entity, string message) removeResponse;
            try
            {
                (ExecutionState executionState, T retrievedEntity, string message) = await GetAsync(id, RetrievalPurpose.Manipulation);

                if (executionState == ExecutionState.Retrieved && retrievedEntity != null)
                {
                    //entity.EntityRemoved(UserId);

                    WriteOnlySet.Remove(retrievedEntity);

                    removeResponse = (executionState: ExecutionState.Deleted, entity: retrievedEntity, message: "Item removed successfully.");
                }
                else
                {
                    removeResponse = (executionState: ExecutionState.Failure, entity: null, message: $"{typeof(T).Name} item not found.");
                }
            }
            catch (Exception ex)
            {
                removeResponse = (executionState: ExecutionState.Failure, entity: null, message: ex.Message.ToString());
            }

            return removeResponse;
        }

        public virtual async Task<(ExecutionState executionState, long entityCount, string message)> CountAsync(ICountOptions<T> countOptions = null)
        {
            (ExecutionState executionState, long entityCount, string message) countResponse;
            long count = 0;

            if (countOptions != null)
            {
                Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>> includeExpression = countOptions.IncludeExpression;
                Expression<Func<T, bool>> filterExpression = countOptions.FilterExpression;

                IQueryable<T> query = ReadOnlySet;
                query = query.Where(x => x.IsDeleted != true);
                //query = query.Where(x => x.IsDeleted != 1);
                if (countOptions.ListCondition == Common.Enum.List.IncludeInactives)
                {
                    query = ReadOnlySet.IgnoreQueryFilters();
                    query = query.Where(x => !x.IsDeleted);
                    //query = query.Where(x => x.IsDeleted == 0);
                }

                // apply include
                if (includeExpression != null)
                {
                    query = (includeExpression.Compile())(query);
                }

                // apply filter
                if (filterExpression != null)
                {
                    query = query?.AsEnumerable()
                        .Where(filterExpression.Compile())
                        .AsQueryable();
                }

                try
                {
                    count = (query != null) ?
                            //await
                            query
                                //.AsAsyncQueryable()
                                .Count() :
                        0;
                }
                catch (Exception ex)
                {
                    countResponse = (executionState: ExecutionState.Failure, entityCount: 0, message: ex.Message.ToString());
                }
            }
            else
            {
                countResponse = (executionState: ExecutionState.Failure, entityCount: 0, message: "Count options not found.");
            }

            countResponse = (executionState: ExecutionState.Success, entityCount: count, message: "Item found");
            return countResponse;
        }

        public virtual async Task<(ExecutionState executionState, T entity, string message)> MarkAsActiveAsync(long id)
        {
            (ExecutionState executionState, T entity, string message) updateRespose;
            (ExecutionState executionState, T entity, string message) = await GetAsync(id, RetrievalPurpose.Manipulation);

            if (executionState == ExecutionState.Retrieved && entity != null)
            {
                WriteOnlyCtx.Attach(entity);        //TODO: verify if this attach is indeed necessary

                //entity.EntityActivated();

                // entity.EntityUpdated ();  
                entity.IsActive = true;
                //entity.IsActive = 1;
                WriteOnlyCtx.Entry(entity).Property(x => x.IsActive).IsModified = true;
            }

            updateRespose = (entity != null)
                ? (executionState: ExecutionState.Activated, entity: entity, message: "Item is marked as active.")
                : (executionState: ExecutionState.Failure, entity: null, message: $"{typeof(T).Name} item not found.");
            return updateRespose;
        }

        public virtual async Task<(ExecutionState executionState, T entity, string message)> MarkAsInactiveAsync(long id)
        {
            (ExecutionState executionState, T entity, string message) updateResponse;
            (ExecutionState executionState, T entity, string message) = await GetAsync(id, RetrievalPurpose.Manipulation);

            if (executionState == ExecutionState.Retrieved && entity != null)
            {
                WriteOnlyCtx.Attach(entity);        //TODO: verify if this attach is indeed necessary

                //entity.EntityInactivated(UserId);

                // entity.EntityUpdated ();  

                entity.IsActive = false;
                //entity.IsActive = 0;
                WriteOnlyCtx.Entry(entity).Property(x => x.IsActive).IsModified = true;
            }

            updateResponse = (entity != null)
                ? (executionState: ExecutionState.Inactivated, entity: entity, message: $"{typeof(T).Name} is marked as in-active.")
                : (executionState: ExecutionState.Failure, entity: null, message: $"{typeof(T).Name} item not found.");
            return updateResponse;
        }

        public virtual void Dispose()
        {
            WriteOnlyCtx?.Dispose();
            ReadOnlyCtx?.Dispose();

            GC.SuppressFinalize(this);
        }

        public async Task<(ExecutionState executionState, bool isDeleted, string message)> SoftDeleteAsync(long key, string userId)
        {
            var rowsEffected = await WriteOnlySet
                .Where(x => x.Id == key)
                .ExecuteUpdateAsync<T>(x =>
                    x.SetProperty(y => y.DeletedAt, DateTime.Now)
                    .SetProperty(y => y.DeletedBy, userId)
                    .SetProperty(y => y.IsDeleted, true));

            return (ExecutionState.Deleted, rowsEffected > 0, "Deleted");
        }
    }
}
