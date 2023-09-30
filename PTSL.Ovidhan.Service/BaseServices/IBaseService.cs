using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PTSL.Ovidhan.Common.Entity.CommonEntity;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.Implementation;
using PTSL.Ovidhan.Common.Model.BaseModels;
using PTSL.Ovidhan.Common.QuerySerialize.Implementation;

namespace PTSL.Ovidhan.Service.BaseServices
{
    public interface IBaseService<T, TEntity> where T : BaseModel, new() where TEntity : BaseEntity
    {
        Task<(ExecutionState executionState, T entity, string message)> CreateAsync(T entity);
        Task<(ExecutionState executionState, IList<T> entity, string message)> CreateAsync(IList<T> entity);
        Task<(ExecutionState executionState, T entity, string message)> GetAsync(long key);
        Task<(ExecutionState executionState, T entity, string message)> GetAsync(FilterOptions<T> filterOptions);
        Task<(ExecutionState executionState, IList<T> entity, string message)> List();
        Task<(ExecutionState executionState, string message)> DoesExistAsync(long key);
        Task<(ExecutionState executionState, string message)> DoesExistAsync(FilterOptions<T> filterOptions);
        Task<(ExecutionState executionState, T entity, string message)> UpdateAsync(T entity);
        Task<(ExecutionState executionState, T entity, string message)> RemoveAsync(long key);
        Task<(ExecutionState executionState, long entityCount, string message)> CountAsync(CountOptions<T> countOptions = null);
        Task<(ExecutionState executionState, T entity, string message)> MarkAsActiveAsync(long key);
        Task<(ExecutionState executionState, T entity, string message)> MarkAsInactiveAsync(long key);
        Task<(ExecutionState executionState, bool isDeleted, string message)> SoftDeleteAsync(long key, string userId);

        TEntity CastModelToEntity(T model);
        T CastEntityToModel(TEntity entity);
        IList<T> CastEntityToModel(IQueryable<TEntity> entity);
        PagedData<T> CastEntityToPagedModel(PagedData<TEntity> entity);
        Task<(ExecutionState executionState, PagedData<T> data, string message)> Page(Pagination pagination);
        //IList<T> CastEntityToModelList(IQueryable<TEntity> entity);
    }
}
