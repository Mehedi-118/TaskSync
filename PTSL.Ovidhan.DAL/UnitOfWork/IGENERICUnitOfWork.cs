using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Storage;

using PTSL.Ovidhan.Common.Entity.CommonEntity;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.Implementation;
using PTSL.Ovidhan.Common.QuerySerialize.Implementation;
using PTSL.Ovidhan.DAL.Repositories.Interface;

namespace PTSL.Ovidhan.DAL.UnitOfWork
{
    public interface IGENERICUnitOfWork : IDisposable
    {
        IDbContextTransaction Begin();
        void Complete(IDbContextTransaction transaction, CompletionState completionState);

        IBaseRepository<T> Select<T>(T entity) where T : BaseEntity;
        IBaseRepository<T> Select<T>() where T : BaseEntity;

        Task<(ExecutionState executionState, string message)> SaveAsync(IDbContextTransaction transaction);
        Task<(ExecutionState executionState, T entity, string message)> CreateAsync<T>(T entity) where T : BaseEntity;
        Task<(ExecutionState executionState, IList<T> entity, string message)> CreateAsync<T>(IList<T> entity) where T : BaseEntity;
        Task<(ExecutionState executionState, T entity, string message)> GetAsync<T>(long id) where T : BaseEntity;
        Task<(ExecutionState executionState, string message)> DoesExistAsync<T>(long id) where T : BaseEntity;

        Task<(ExecutionState executionState, T entity, string message)> GetAsync<T>(FilterOptions<T> filterOptions, RetrievalPurpose retrievalPurpose = RetrievalPurpose.Consumption)
            where T : BaseEntity;

        Task<(ExecutionState executionState, IQueryable<T> entity, string message)> List<T>(
            QueryOptions<T> queryOptions = null)
            where T : BaseEntity;

        Task<(ExecutionState executionState, string message)> DoesExistAsync<T>(
            FilterOptions<T> filterOptions)
            where T : BaseEntity;

        Task<(ExecutionState executionState, T entity, string message)> UpdateAsync<T>(T entity)
            where T : BaseEntity;

        Task<(ExecutionState executionState, IList<T> entity, string message)> UpdateAsync<T>(IList<T> entity)
            where T : BaseEntity;

        Task<(ExecutionState executionState, T entity, string message)> RemoveAsync<T>(long id)
            where T : BaseEntity;

        Task<(ExecutionState executionState, long entityCount, string message)> CountAsync<T>(
            CountOptions<T> countOptions = null)
            where T : BaseEntity;

        Task<(ExecutionState executionState, T entity, string message)> MarkAsActiveAsync<T>(long id)
            where T : BaseEntity;

        Task<(ExecutionState executionState, T entity, string message)> MarkAsInactiveAsync<T>(long id)
            where T : BaseEntity;

        Task<(ExecutionState executionState, bool isDeleted, string message)> SoftDeleteAsync<T>(long id, string userId)
            where T : BaseEntity;
        Task<(ExecutionState executionState, PagedData<T> data, string message)> Page<T>(QueryOptions<T> queryOptions = null) where T : BaseEntity;
    }
}
