using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PTSL.Ovidhan.Common.Entity.Tasks;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.Ovidhan.Common.QuerySerialize.Implementation;
using PTSL.Ovidhan.Service.BaseServices;

namespace PTSL.Ovidhan.Service.Services.Interface.GeneralSetup
{
    public interface ITodoService : IBaseService<TodoVM, Todo>
    {
        Task<(ExecutionState executionState, IList<TodoVM> entity, string message)> List(string userId, QueryOptions<Todo> queryOptions = null);
        Task<(ExecutionState executionState, TodoVM entity, string message)> GetTasksWithRemindersByUserId(string key);
        Task<(ExecutionState executionState, IList<TodoVM> entity, string message)> GetByCategoryIdAsync(long  key);
    }

   
}