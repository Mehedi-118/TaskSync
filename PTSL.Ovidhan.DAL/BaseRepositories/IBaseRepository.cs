using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Storage;

using PTSL.Ovidhan.Common.Entity.CommonEntity;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.Implementation;
using PTSL.Ovidhan.Common.QuerySerialize.Interfaces;

namespace PTSL.Ovidhan.DAL.Repositories.Interface
{
    public interface IBaseRepository<T> : IDisposable where T : BaseEntity
    {
        Task<bool> SaveAsync(IDbContextTransaction transaction);
        Task<(ExecutionState executionState, T entity, string message)> CreateAsync(T entity);
        Task<(ExecutionState executionState, IList<T> entity, string message)> CreateAsync(IList<T> entity);
        Task<(ExecutionState executionState, T entity, string message)> GetAsync(long key, RetrievalPurpose retrievalPurpose = RetrievalPurpose.Consumption);
        Task<(ExecutionState executionState, T entity, string message)> GetAsync(IFilterOptions<T> filterOptions, RetrievalPurpose retrievalPurpose = RetrievalPurpose.Consumption);
        Task<(ExecutionState executionState, IQueryable<T> entity, string message)> List(IQueryOptions<T> queryOptions = null);
        Task<(ExecutionState executionState, string message)> DoesExistAsync(long key);
        Task<(ExecutionState executionState, string message)> DoesExistAsync(IFilterOptions<T> filterOptions);
        Task<(ExecutionState executionState, T entity, string message)> UpdateAsync(T entity);
        Task<(ExecutionState executionState, IList<T> entity, string message)> UpdateAsync(IList<T> entity);
        Task<(ExecutionState executionState, T entity, string message)> RemoveAsync(long key);
        Task<(ExecutionState executionState, long entityCount, string message)> CountAsync(ICountOptions<T> countOptions = null);
        Task<(ExecutionState executionState, T entity, string message)> MarkAsActiveAsync(long key);
        Task<(ExecutionState executionState, T entity, string message)> MarkAsInactiveAsync(long key);
        Task<(ExecutionState executionState, bool isDeleted, string message)> SoftDeleteAsync(long key, string userId);
        Task<(ExecutionState executionState, PagedData<T> data, string message)> Page(IQueryOptions<T> queryOptions = null);
    }
}
