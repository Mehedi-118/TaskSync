using Microsoft.EntityFrameworkCore.Storage;
using PTSL.Ovidhan.Common.Entity.CommonEntity;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.QuerySerialize.Implementation;
using PTSL.Ovidhan.DAL.UnitOfWork;
using PTSL.Ovidhan.Common.Implementation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.Ovidhan.Business.BaseBusinesses
{
    public abstract class BaseBusiness<T> : IBaseBusiness<T>, IDisposable where T : BaseEntity
    {
        protected IGENERICUnitOfWork UoW;
        public BaseBusiness(IGENERICUnitOfWork uoW)
        {
            UoW = uoW;
        }

        public virtual async Task<(ExecutionState executionState, T entity, string message)> CreateAsync(T entity)
        {
            (ExecutionState executionState, T entity, string message) createResponse;
            ////entity = entity.Trim<T, Guid> ();

            //#region Pre validation
            //(ExecutionState executionState, string message) validateResponse = PreValidation.Creating(entity);
            //#endregion
            //if (validateResponse.executionState == ExecutionState.Failure)
            //{
            //    createResponse = (validateResponse.executionState, entity: null, validateResponse.message);
            //}
            //else
            //{
            await using (IDbContextTransaction transaction = UoW.Begin())
            {
                try
                {
                    (ExecutionState executionState, T entity, string message) createdResponse = await UoW.CreateAsync<T>(entity);

                    if (createdResponse.executionState == ExecutionState.Failure)
                    {
                        if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid validTransactionGuid))
                        {
                            UoW.Complete(transaction, CompletionState.Failure);
                        }

                        createResponse = createdResponse;
                    }
                    else
                    {
                        (ExecutionState executionState, string message) saveResponse = await UoW.SaveAsync(transaction);

                        bool success = (saveResponse.executionState == ExecutionState.Success);

                        //createResponse = success ?
                        //                createdResponse :
                        //                (executionState: saveResponse.executionState, entity: null, message: saveResponse.message);
                        #region Post validation
                        if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
                        {
                            UoW.Complete(transaction, success ? CompletionState.Success : CompletionState.Failure);

                            createResponse = success ?
                                        createdResponse :
                                        (executionState: saveResponse.executionState, entity: null, message: saveResponse.message);

                            //validateResponse = PostValidation.Created(createdResponse.entity);

                            //if (validateResponse.executionState == ExecutionState.Failure)
                            //{
                            //    createResponse = (executionState: ExecutionState.Failure, entity: null, validateResponse.message);
                            //}
                            //else
                            //{
                            //    createResponse = success ?
                            //        createdResponse :
                            //        (executionState: saveResponse.executionState, entity: null, message: saveResponse.message);
                            //}
                        }
                        else
                        {
                            createResponse = (executionState: ExecutionState.Failure, entity: null, message: "Transaction not found.");
                        }
                        #endregion
                    }
                }
                catch
                {
                    if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
                    {
                        UoW.Complete(transaction, CompletionState.Failure);
                    }

                    createResponse = (executionState: ExecutionState.Failure, entity: null, message: $"Problem on {typeof(T).ToString()} creation.");
                }
            }
            //}

            return createResponse;
        }
        public virtual async Task<(ExecutionState executionState, T entity, string message)> GetAsync(long key)
        {
            (ExecutionState executionState, T entity, string message) returnResponse;

            //(ExecutionState executionState, string message) validateKey = PreValidation.Getting(key);

            //if (validateKey.executionState == ExecutionState.Failure)
            //{
            //    returnResponse = (executionState: ExecutionState.Failure, entity: null, message: validateKey.message);
            //}
            //else
            //{
            (ExecutionState executionState, T entity, string message) entityObject = await UoW.GetAsync<T>(key);

            if (entityObject.entity != null)
            {
                returnResponse = entityObject;

                //validateKey = PostValidation.Got(entityObject.entity);

                //if (validateKey.executionState == ExecutionState.Failure)
                //{
                //    returnResponse = (executionState: ExecutionState.Failure, entity: null, message: validateKey.message);
                //}
                //else
                //{
                //    returnResponse = entityObject;
                //}
            }
            else
            {
                returnResponse = entityObject;
            }
            //}

            return returnResponse;
        }
        public virtual async Task<(ExecutionState executionState, T entity, string message)> GetAsync(FilterOptions<T> filterOptions)
        {
            (ExecutionState executionState, T entity, string message) returnResponse;

            //(ExecutionState executionState, string message) = PreValidation.Getting(filterOptions);

            //if (executionState == ExecutionState.Failure)
            //{
            //    returnResponse = (executionState: ExecutionState.Failure, entity: null, message: message);
            //}
            //else
            //{

            (ExecutionState executionState, T entity, string message) entityObject = await UoW.GetAsync<T>(filterOptions);
            returnResponse = entityObject;

            //(ExecutionState executionState, string message) validateKey = PostValidation.Got(entityObject.entity);

            //if (validateKey.executionState == ExecutionState.Failure)
            //{
            //    returnResponse = (executionState: ExecutionState.Failure, entity: null, message: validateKey.message);
            //}
            //else
            //{
            //    returnResponse = entityObject;
            //}
            //}

            return returnResponse;
        }
        public virtual async Task<(ExecutionState executionState, IQueryable<T> entity, string message)> List(QueryOptions<T> queryOptions = null)
        {
            (ExecutionState executionState, IQueryable<T> entity, string message) returnResponse;

            //(ExecutionState executionState, string message) validateList = PreValidation.Listing(queryOptions);

            //if (validateList.executionState == ExecutionState.Failure)
            //{
            //    returnResponse = (executionState: ExecutionState.Failure, entity: null, validateList.message);
            //}
            //else
            //{
            (ExecutionState executionState, IQueryable<T> entity, string message) entityObject = await UoW.List<T>(queryOptions);
            returnResponse = entityObject;
            //validateList = PostValidation.Listed(entityObject.entity);

            //if (validateList.executionState == ExecutionState.Failure)
            //{
            //    returnResponse = (executionState: ExecutionState.Failure, entity: null, validateList.message);
            //}
            //else
            //{
            //    returnResponse = entityObject;
            //}
            //}

            return returnResponse;
        }

        public virtual async Task<(ExecutionState executionState, PagedData<T> data, string message)> Page(Pagination pagination, QueryOptions<T> queryOptions = null)
        {
            (ExecutionState executionState, PagedData<T> data, string message) returnResponse;

            //(ExecutionState executionState, string message) validateList = PreValidation.Listing(queryOptions);

            //if (validateList.executionState == ExecutionState.Failure)
            //{
            //    returnResponse = (executionState: ExecutionState.Failure, entity: null, validateList.message);
            //}
            //else
            //{

            queryOptions = queryOptions ?? new QueryOptions<T>();
            queryOptions.Pagination = pagination;

            (ExecutionState executionState, PagedData<T> data, string message) entityObject = await UoW.Page<T>(queryOptions);
            returnResponse = entityObject;
            //validateList = PostValidation.Listed(entityObject.entity);

            //if (validateList.executionState == ExecutionState.Failure)
            //{
            //    returnResponse = (executionState: ExecutionState.Failure, entity: null, validateList.message);
            //}
            //else
            //{
            //    returnResponse = entityObject;
            //}
            //}

            return returnResponse;
        }

        public virtual async Task<(ExecutionState executionState, string message)> DoesExistAsync(long key)
        {
            (ExecutionState executionState, string message) returnResponse;

            returnResponse = await UoW.DoesExistAsync<T>(key);

            //(ExecutionState executionState, string message) = PreValidation.DoesExist(key);

            //if (executionState == ExecutionState.Failure)
            //{
            //    returnResponse = (executionState: ExecutionState.Failure, message: message);
            //}
            //else
            //{
            //    returnResponse = await UoW.DoesExistAsync<T>(key);
            //}

            return returnResponse;
        }
        public virtual async Task<(ExecutionState executionState, string message)> DoesExistAsync(FilterOptions<T> filterOptions)
        {
            (ExecutionState executionState, string message) returnResponse;

            returnResponse = await UoW.DoesExistAsync<T>(filterOptions);

            //(ExecutionState executionState, string message) = PreValidation.DoesExist(filterOptions);

            //if (executionState == ExecutionState.Failure)
            //{
            //    returnResponse = (executionState: ExecutionState.Failure, message: message);
            //}
            //else
            //{
            //    returnResponse = await UoW.DoesExistAsync<T>(filterOptions);
            //}

            return returnResponse;
        }
        public virtual async Task<(ExecutionState executionState, T entity, string message)> UpdateAsync(T entity)
        {
            (ExecutionState executionState, T entity, string message) updateResponse;
            //(ExecutionState executionState, string message) validateResponse = PreValidation.Updating(entity);

            //if (validateResponse.executionState == ExecutionState.Failure)
            //{
            //    updateResponse = (validateResponse.executionState, entity: null, validateResponse.message);
            //}
            //else
            //{
            await using (IDbContextTransaction transaction = UoW.Begin())
            {
                try
                {
                    (ExecutionState executionState, T entity, string message) updatedEntity = await UoW.UpdateAsync<T>(entity);

                    (ExecutionState executionState, string message) saveResponse = await UoW.SaveAsync(transaction);

                    bool success = saveResponse.executionState == ExecutionState.Success;

                    if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
                    {
                        UoW.Complete(transaction, success ? CompletionState.Success : CompletionState.Failure);

                        updateResponse = success ? updatedEntity : (executionState: saveResponse.executionState, entity: null, message: saveResponse.message);

                        //validateResponse = PostValidation.Updated(entity);

                        //if (validateResponse.executionState == ExecutionState.Failure)
                        //{

                        //    updateResponse = (validateResponse.executionState, entity: null, validateResponse.message);
                        //}
                        //else
                        //{
                        //    updateResponse = success ? updatedEntity : (executionState: saveResponse.executionState, entity: null, message: saveResponse.message);
                        //}


                    }
                    else
                    {
                        updateResponse = (executionState: ExecutionState.Failure, entity: null, message: "Transaction not found.");
                    }
                }
                catch
                {
                    if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
                    {
                        UoW.Complete(transaction, CompletionState.Failure);
                    }

                    updateResponse = (executionState: ExecutionState.Failure, entity: null, message: $"Problem on {typeof(T).Name} update.");
                }
            }
            //}

            return updateResponse;
        }
        public virtual async Task<(ExecutionState executionState, T entity, string message)> RemoveAsync(long key)
        {
            (ExecutionState executionState, T entity, string message) updateResponse;
            //(ExecutionState executionState, string message) validateResponse = PreValidation.Removing(key);

            //if (validateResponse.executionState == ExecutionState.Failure)
            //{
            //    updateResponse = (validateResponse.executionState, entity: null, validateResponse.message);
            //}
            //else
            //{
            await using (IDbContextTransaction transaction = UoW.Begin())
            {
                try
                {
                    (ExecutionState executionState, T entity, string message) updatedEntity = await UoW.RemoveAsync<T>(key);

                    (ExecutionState executionState, string message) saveResponse = await UoW.SaveAsync(transaction);

                    bool success = saveResponse.executionState == ExecutionState.Success;

                    if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
                    {
                        UoW.Complete(transaction, success ? CompletionState.Success : CompletionState.Failure);

                        updateResponse = success ? updatedEntity : (executionState: saveResponse.executionState, entity: null, message: saveResponse.message);

                        //validateResponse = PostValidation.Removed(key);

                        //if (validateResponse.executionState == ExecutionState.Failure)
                        //{

                        //    updateResponse = (validateResponse.executionState, entity: null, validateResponse.message);
                        //}
                        //else
                        //{
                        //    updateResponse = success ? updatedEntity : (executionState: saveResponse.executionState, entity: null, message: saveResponse.message);
                        //}

                    }
                    else
                    {
                        updateResponse = (executionState: ExecutionState.Failure, entity: null, message: "Transaction not found.");
                    }
                }
                catch
                {
                    if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
                    {
                        UoW.Complete(transaction, CompletionState.Failure);
                    }

                    updateResponse = (executionState: ExecutionState.Failure, entity: null, message: $"Problem on {typeof(T).Name} delete");
                }
            }
            //}
            return updateResponse;
        }
        public virtual async Task<(ExecutionState executionState, long entityCount, string message)> CountAsync(CountOptions<T> countOptions = null)
        {
            (ExecutionState executionState, long entityCount, string message) returnResponse;

            //(ExecutionState executionState, string message) validateCount = PreValidation.Counting(countOptions);

            //if (validateCount.executionState == ExecutionState.Failure)
            //{
            //    returnResponse = (validateCount.executionState, entityCount: 0, validateCount.message);
            //}
            //else
            //{

            (ExecutionState executionState, long entityCount, string message) countEntity = await UoW.CountAsync<T>(countOptions);
            returnResponse = countEntity;

            //validateCount = PostValidation.Count(countEntity.entityCount);

            //if (validateCount.executionState == ExecutionState.Failure)
            //{
            //    returnResponse = (validateCount.executionState, entityCount: 0, validateCount.message);
            //}
            //else
            //{
            //    returnResponse = countEntity;
            //}
            //}

            return returnResponse;
        }
        public virtual async Task<(ExecutionState executionState, T entity, string message)> MarkAsActiveAsync(long key)
        {
            (ExecutionState executionState, T entity, string message) updateResponse;

            //(ExecutionState executionState, string message) validateResponse = PreValidation.MarkingAsActive(key);

            //if (validateResponse.executionState == ExecutionState.Failure)
            //{
            //    updateResponse = (validateResponse.executionState, entity: null, validateResponse.message);
            //}
            //else
            //{

            await using (IDbContextTransaction transaction = UoW.Begin())
            {
                try
                {
                    (ExecutionState executionState, T entity, string message) updatedEntity = await UoW.MarkAsActiveAsync<T>(key);

                    (ExecutionState executionState, string message) saveResponse = await UoW.SaveAsync(transaction);

                    bool success = saveResponse.executionState == ExecutionState.Success;

                    if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
                    {
                        UoW.Complete(transaction, success ? CompletionState.Success : CompletionState.Failure);
                        updateResponse = success ? updatedEntity : (executionState: saveResponse.executionState, entity: null, message: saveResponse.message);

                        //validateResponse = PostValidation.MarkedAsActive(key);

                        //if (validateResponse.executionState == ExecutionState.Failure)
                        //{
                        //    updateResponse = (validateResponse.executionState, entity: null, validateResponse.message);
                        //}
                        //else
                        //{
                        //    updateResponse = success ? updatedEntity : (executionState: saveResponse.executionState, entity: null, message: saveResponse.message);
                        //}
                    }
                    else
                    {
                        updateResponse = (executionState: ExecutionState.Failure, entity: null, message: "Transaction not found.");
                    }
                }
                catch (Exception ex)
                {
                    if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
                    {
                        UoW.Complete(transaction, CompletionState.Failure);
                    }

                    updateResponse = (executionState: ExecutionState.Failure, entity: null, message: $"Problem on {typeof(T).Name} make active.");
                }
            }

            //}

            return updateResponse;
        }
        public virtual async Task<(ExecutionState executionState, T entity, string message)> MarkAsInactiveAsync(long key)
        {
            (ExecutionState executionState, T entity, string message) updateResponse;
            //(ExecutionState executionState, string message) validateResponse = PreValidation.MarkingAsInActive(key);

            //if (validateResponse.executionState == ExecutionState.Failure)
            //{
            //    updateResponse = (validateResponse.executionState, entity: null, validateResponse.message);
            //}
            //else
            //{
            await using (IDbContextTransaction transaction = UoW.Begin())
            {
                try
                {
                    (ExecutionState executionState, T entity, string message) updatedEntity = await UoW.MarkAsInactiveAsync<T>(key);

                    (ExecutionState executionState, string message) saveResponse = await UoW.SaveAsync(transaction);

                    bool success = saveResponse.executionState == ExecutionState.Success;

                    if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
                    {
                        UoW.Complete(transaction, success ? CompletionState.Success : CompletionState.Failure);
                        updateResponse = success ? updatedEntity : (executionState: saveResponse.executionState, entity: null, message: saveResponse.message);

                        //validateResponse = PostValidation.MarkedAsInActive(key);

                        //if (validateResponse.executionState == ExecutionState.Failure)
                        //{
                        //    updateResponse = (validateResponse.executionState, entity: null, validateResponse.message);
                        //}
                        //else
                        //{
                        //    updateResponse = success ? updatedEntity : (executionState: saveResponse.executionState, entity: null, message: saveResponse.message);
                        //}
                    }
                    else
                    {
                        updateResponse = (executionState: ExecutionState.Failure, entity: null, message: "Transaction completed.");
                    }
                }
                catch (Exception ex)
                {
                    if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
                    {
                        UoW.Complete(transaction, CompletionState.Failure);
                    }

                    updateResponse = (executionState: ExecutionState.Failure, entity: null, message: $"Problem on {typeof(T).Name} make inactive");
                }
            }
            //}

            return updateResponse;
        }

        public void Dispose()
        {
            UoW?.Dispose();

            GC.SuppressFinalize(this);
        }

        public Task<(ExecutionState executionState, bool isDeleted, string message)> SoftDeleteAsync(long key, string userId)
        {
            return UoW.SoftDeleteAsync<T>(key, userId);
        }

        public async Task<(ExecutionState executionState, IList<T> entity, string message)> CreateAsync(IList<T> entity)
        {
            (ExecutionState executionState, IList<T> entity, string message) createResponse;
            
            await using (IDbContextTransaction transaction = UoW.Begin())
            {
                try
                {
                    (ExecutionState executionState, IList<T> entity, string message) createdResponse = await UoW.CreateAsync<T>(entity);

                    if (createdResponse.executionState == ExecutionState.Failure)
                    {
                        if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid validTransactionGuid))
                        {
                            UoW.Complete(transaction, CompletionState.Failure);
                        }

                        createResponse = createdResponse;
                    }
                    else
                    {
                        (ExecutionState executionState, string message) saveResponse = await UoW.SaveAsync(transaction);

                        bool success = (saveResponse.executionState == ExecutionState.Success);

                        #region Post validation
                        if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
                        {
                            UoW.Complete(transaction, success ? CompletionState.Success : CompletionState.Failure);

                            createResponse = success ?
                                        createdResponse :
                                        (executionState: saveResponse.executionState, entity: new List<T>(), message: saveResponse.message);

                        }
                        else
                        {
                            createResponse = (executionState: ExecutionState.Failure, entity: new List<T>(), message: "Transaction not found.");
                        }
                        #endregion
                    }
                }
                catch
                {
                    if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
                    {
                        UoW.Complete(transaction, CompletionState.Failure);
                    }

                    createResponse = (executionState: ExecutionState.Failure, entity: new List<T>(), message: $"Problem on {typeof(T).ToString()} creation.");
                }
            }
            //}

            return createResponse;
        }

        public async Task<(ExecutionState executionState, IList<T> entity, string message)> UpdateAsync(IList<T> entity)
        {
            (ExecutionState executionState, IList<T> entity, string message) updateResponse;

            await using (IDbContextTransaction transaction = UoW.Begin())
            {
                try
                {
                    (ExecutionState executionState, IList<T> entity, string message) updatedResponse = await UoW.UpdateAsync<T>(entity);

                    if (updatedResponse.executionState == ExecutionState.Failure)
                    {
                        if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid validTransactionGuid))
                        {
                            UoW.Complete(transaction, CompletionState.Failure);
                        }

                        updateResponse = updatedResponse;
                    }
                    else
                    {
                        (ExecutionState executionState, string message) saveResponse = await UoW.SaveAsync(transaction);

                        bool success = (saveResponse.executionState == ExecutionState.Success);

                        #region Post validation
                        if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
                        {
                            UoW.Complete(transaction, success ? CompletionState.Success : CompletionState.Failure);

                            updateResponse = success ?
                                        updatedResponse :
                                        (executionState: saveResponse.executionState, entity: new List<T>(), message: saveResponse.message);

                        }
                        else
                        {
                            updateResponse = (executionState: ExecutionState.Failure, entity: new List<T>(), message: "Transaction not found.");
                        }
                        #endregion
                    }
                }
                catch
                {
                    if (Guid.TryParse(transaction.TransactionId.ToString(), out Guid transactionGuid))
                    {
                        UoW.Complete(transaction, CompletionState.Failure);
                    }

                    updateResponse = (executionState: ExecutionState.Failure, entity: new List<T>(), message: $"Problem on {typeof(T).ToString()} creation.");
                }
            }
            //}

            return updateResponse;
        }
    }
}
