using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Storage;

using PTSL.Ovidhan.Common.Entity;
using PTSL.Ovidhan.Common.Entity.CommonEntity;
using PTSL.Ovidhan.Common.Entity.GeneralSetup;
using PTSL.Ovidhan.Common.Entity.Tasks;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.Implementation;
using PTSL.Ovidhan.Common.QuerySerialize.Implementation;
using PTSL.Ovidhan.DAL.Repositories.Interface;
using PTSL.Ovidhan.DAL.Repositories.Interface.GeneralSetup;

namespace PTSL.Ovidhan.DAL.UnitOfWork
{
    public class GENERICUnitOfWork : IGENERICUnitOfWork
    {
        private GENERICWriteOnlyCtx WriteOnlyCtx { get; }
        private GENERICReadOnlyCtx ReadOnlyCtx { get; }
        private ICategoryRepository CategoryRepository { get; }
        private ITodoRepository TodoRepository { get; }
        private IReminderRepository ReminderRepository { get; }



       
        public GENERICUnitOfWork(
            GENERICWriteOnlyCtx ecommarceWriteOnlyCtx,
            GENERICReadOnlyCtx ecommarceReadOnlyCtx,
            ICategoryRepository categoryRepository,
            ITodoRepository todoRepository,
            IReminderRepository reminderRepository
            )
        {
            WriteOnlyCtx = ecommarceWriteOnlyCtx;
            ReadOnlyCtx = ecommarceReadOnlyCtx;
            CategoryRepository = categoryRepository;
            TodoRepository =todoRepository;
            ReminderRepository=reminderRepository;
        }

        public IDbContextTransaction Begin()
        {
            try
            {
                IDbContextTransaction transaction = WriteOnlyCtx.Database.BeginTransaction();
                return transaction;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Complete(IDbContextTransaction transaction, CompletionState completionState)
        {
            try
            {
                if (transaction != null && transaction.TransactionId != null && transaction.GetDbTransaction() != null)
                {
                    if (completionState == CompletionState.Success)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
            }
            catch
            {
                transaction.Rollback();
            }
        }

        #region Select a Repository based on given type

        public IBaseRepository<T> Select<T>(T entity) where T : BaseEntity
        {
            IBaseRepository<T> repository = default(IBaseRepository<T>);
            switch (entity)
            {
                case Category _:
                    repository = (IBaseRepository<T>)CategoryRepository;
                    break;
                case Todo _:
                    repository = (IBaseRepository<T>)TodoRepository;
                    break;
                case Reminder _:
                    repository = (IBaseRepository<T>)ReminderRepository;
                    break;

            }
            return repository;
        }

        public IBaseRepository<T> Select<T>() where T : BaseEntity
        {
            IBaseRepository<T> repository = default(IBaseRepository<T>);

            Type type = typeof(T);
            if (type == typeof(Category))
            {
                repository = (IBaseRepository<T>)CategoryRepository;
            }
            else if (type == typeof(Todo))
            {
                repository = (IBaseRepository<T>)TodoRepository;
            }
            else if (type == typeof(Reminder))
            {
                repository = (IBaseRepository<T>)ReminderRepository;
            }


            return repository;
        }

        public virtual async Task<(ExecutionState executionState, string message)> SaveAsync(IDbContextTransaction transaction)
        {
            if (transaction != null)
            {
                if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
                {
                    try
                    {
                        await WriteOnlyCtx.SaveChangesAsync();
                        return (executionState: ExecutionState.Success, message: "Transaction completed.");
                    }
                    catch (Exception ex)
                    {
                        return (executionState: ExecutionState.Failure, message: ex.Message);
                    }
                }
                else
                {
                    return (executionState: ExecutionState.Failure, message: "Transaction not found.");
                }
            }
            else
            {
                return (executionState: ExecutionState.Failure, message: "Transaction not found.");
            }
        }
        public async Task<(ExecutionState executionState, T entity, string message)> CreateAsync<T>(T entity) where T : BaseEntity
        {
            IBaseRepository<T> repository = Select(entity);

            (ExecutionState executionState, T entity, string message) createdEntity = await repository.CreateAsync(entity);

            return createdEntity;
        }

        public async Task<(ExecutionState executionState, IList<T> entity, string message)> CreateAsync<T>(IList<T> entity) where T : BaseEntity
        {
            IBaseRepository<T> repository = Select<T>();

            (ExecutionState executionState, IList<T> entity, string message) createdEntity = await repository.CreateAsync(entity);

            return createdEntity;
        }

        public async Task<(ExecutionState executionState, T entity, string message)> GetAsync<T>(long id) where T : BaseEntity
        {
            IBaseRepository<T> repository = Select<T>();
            (ExecutionState executionState, T entity, string message) retrievedEntity = await repository.GetAsync(id);
            return retrievedEntity;
        }
        public async Task<(ExecutionState executionState, T entity, string message)> UpdateAsync<T>(T entity) where T : BaseEntity
        {
            IBaseRepository<T> repository = Select(entity);
            (ExecutionState executionState, T entity, string message) updateEntity = await repository.UpdateAsync(entity);
            return updateEntity;
        }

        public async Task<(ExecutionState executionState, IList<T> entity, string message)> UpdateAsync<T>(IList<T> entity) where T : BaseEntity
        {
            IBaseRepository<T> repository = Select<T>();

            (ExecutionState executionState, IList<T> entity, string message) createdEntity = await repository.UpdateAsync(entity);

            return createdEntity;
        }

        public async Task<(ExecutionState executionState, T entity, string message)> RemoveAsync<T>(long id) where T : BaseEntity
        {
            IBaseRepository<T> repository = Select<T>();
            (ExecutionState executionState, T entity, string message) removeEntity = await repository.RemoveAsync(id);
            return removeEntity;
        }
        public async Task<(ExecutionState executionState, T entity, string message)> GetAsync<T>
            (FilterOptions<T> filterOptions, RetrievalPurpose retrievalPurpose = RetrievalPurpose.Consumption)
            where T : BaseEntity
        {
            IBaseRepository<T> repository = Select<T>();

            (ExecutionState retrievedEntityExecutionState, T retrievedEntity, string retrievedEntityMessage) =
                await repository.GetAsync(filterOptions, retrievalPurpose);

            return (executionState: retrievedEntityExecutionState, entity: retrievedEntity, message: retrievedEntityMessage);
        }
        public async Task<(ExecutionState executionState, IQueryable<T> entity, string message)> List<T>(QueryOptions<T> queryOptions = null) where T : BaseEntity
        {
            IBaseRepository<T> repository = Select<T>();

            (ExecutionState retrievedEntitiesExecutionState, IQueryable<T> retrievedEntities, string retrievedEntitiesMessage) =
                await repository.List(queryOptions);

            return (executionState: retrievedEntitiesExecutionState, entity: retrievedEntities, message: retrievedEntitiesMessage);
        }

        public async Task<(ExecutionState executionState, PagedData<T> data, string message)> Page<T>(QueryOptions<T> queryOptions = null) where T : BaseEntity
        {
            IBaseRepository<T> repository = Select<T>();

            (ExecutionState retrievedEntitiesExecutionState, PagedData<T> retrievedData, string retrievedEntitiesMessage) =
                await repository.Page(queryOptions);

            return (executionState: retrievedEntitiesExecutionState, data: retrievedData, message: retrievedEntitiesMessage);
        }

        public async Task<(ExecutionState executionState, string message)> DoesExistAsync<T>(FilterOptions<T> filterOptions)
            where T : BaseEntity
        {
            IBaseRepository<T> repository = Select<T>();

            (ExecutionState entitityExistsExecutionState, string entitityExistsMessage) =
                await repository.DoesExistAsync(filterOptions);

            return (executionState: entitityExistsExecutionState, message: entitityExistsMessage);
        }
        public async Task<(ExecutionState executionState, long entityCount, string message)> CountAsync<T>(CountOptions<T> countOptions = null)
            where T : BaseEntity
        {
            IBaseRepository<T> repository = Select<T>();

            (ExecutionState entitiesCountExecutionState, long entitiesCount, string entityCountMessage) =
                await repository.CountAsync(countOptions);

            return (executionState: entitiesCountExecutionState, entityCount: entitiesCount, message: entityCountMessage);
        }
        public async Task<(ExecutionState executionState, T entity, string message)> MarkAsActiveAsync<T>(long id) where T : BaseEntity
        {
            IBaseRepository<T> repository = Select<T>();
            (ExecutionState executionState, T entity, string message) activeEntity = await repository.MarkAsActiveAsync(id);
            return activeEntity;
        }
        public async Task<(ExecutionState executionState, T entity, string message)> MarkAsInactiveAsync<T>(long id) where T : BaseEntity
        {
            IBaseRepository<T> repository = Select<T>();
            (ExecutionState executionState, T entity, string message) inactiveEntity = await repository.MarkAsInactiveAsync(id);
            return inactiveEntity;
        }
        public async Task<(ExecutionState executionState, string message)> DoesExistAsync<T>(long id) where T : BaseEntity
        {
            IBaseRepository<T> repository = Select<T>();
            (ExecutionState executionState, string message) doesExist = await repository.DoesExistAsync(id);
            return doesExist;
        }

        #endregion

        public void Dispose()
        {
            WriteOnlyCtx?.Dispose();
            ReadOnlyCtx?.Dispose();

            GC.SuppressFinalize(this);
        }

        public Task<(ExecutionState executionState, bool isDeleted, string message)> SoftDeleteAsync<T>(long id, string userId) where T : BaseEntity
        {
            IBaseRepository<T> repository = Select<T>();

            return repository.SoftDeleteAsync(id, userId);
        }

        public static implicit operator GENERICUnitOfWork(GENERICReadOnlyCtx v)
        {
            throw new NotImplementedException();
        }
    }
}
