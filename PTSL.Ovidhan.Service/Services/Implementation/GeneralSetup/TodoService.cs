using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.EntityFrameworkCore.Metadata.Internal;

using PTSL.Ovidhan.Business.Businesses.Interface.GeneralSetup;
using PTSL.Ovidhan.Common.Entity.Tasks;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.Ovidhan.Common.QuerySerialize.Implementation;
using PTSL.Ovidhan.Service.BaseServices;
using PTSL.Ovidhan.Service.Services.Interface.GeneralSetup;

namespace PTSL.GENERIC.Service.Services.Output
{
    public class TodoService : BaseService<TodoVM, Todo>, ITodoService
    {
        private readonly ITodoBusiness _business;
        public IMapper _mapper;

        public TodoService(ITodoBusiness business, IMapper mapper) : base(business, mapper)
        {
            _business = business;
            _mapper = mapper;
        }

        public override Todo CastModelToEntity(TodoVM model)
        {
            return _mapper.Map<Todo>(model);
        }

        public override TodoVM CastEntityToModel(Todo entity)
        {
            return _mapper.Map<TodoVM>(entity);
        }

        public override IList<TodoVM> CastEntityToModel(IQueryable<Todo> entity)
        {
            return _mapper.Map<IList<TodoVM>>(entity).ToList();
        }

        public async Task<(ExecutionState executionState, TodoVM entity, string message)> GetTasksWithRemindersByUserId(string key)
        {
            try
            {
                (ExecutionState executionState, Todo entity, string message) todo = await _business.GetTasksWithRemindersByUserId(key);

                if (todo.executionState == ExecutionState.Retrieved)
                {
                    return (ExecutionState.Success, CastEntityToModel(todo.entity), todo.message);

                }
                return (ExecutionState.Failure, CastEntityToModel(todo.entity), todo.message);
            }
            catch (Exception ex)
            {
                return (ExecutionState.Failure, null, null);

            }

        }

        public async Task<(ExecutionState executionState, IList<TodoVM> entity, string message)> List(string userId, QueryOptions<Todo> queryOptions = null)
        {
            try
            {
                (ExecutionState executionState, IQueryable<Todo> entity, string message) todo = await _business.List(userId);

                if (todo.executionState == ExecutionState.Retrieved)
                {
                    return (ExecutionState.Success, CastEntityToModel(todo.entity), todo.message);

                }
                return (ExecutionState.Failure, CastEntityToModel(todo.entity), todo.message);
            }
            catch (Exception ex)
            {
                return (ExecutionState.Failure, null, null);

            }
        }

        public async Task<(ExecutionState executionState, IList<TodoVM> entity, string message)> GetByCategoryIdAsync(long key)
        {
            try
            {
                (ExecutionState executionState, IQueryable<Todo> entity, string message) todo = await _business.List(key);

                if (todo.executionState == ExecutionState.Retrieved)
                {
                    return (ExecutionState.Success, CastEntityToModel(todo.entity), todo.message);

                }
                return (ExecutionState.Failure, CastEntityToModel(todo.entity), todo.message);
            }
            catch (Exception ex)
            {
                return (ExecutionState.Failure, null, null);

            }
        }
    }
}