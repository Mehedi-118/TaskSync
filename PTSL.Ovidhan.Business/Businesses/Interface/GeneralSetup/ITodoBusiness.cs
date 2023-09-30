using System.Linq;
using System.Threading.Tasks;

using PTSL.Ovidhan.Business.BaseBusinesses;
using PTSL.Ovidhan.Common.Entity.Tasks;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.QuerySerialize.Implementation;

namespace PTSL.Ovidhan.Business.Businesses.Interface.GeneralSetup
{
    public interface ITodoBusiness : IBaseBusiness<Todo>
    {
        Task<(ExecutionState executionState, Todo entity, string message)> GetTasksWithRemindersByUserId(string key);

        Task<(ExecutionState executionState, IQueryable<Todo> entity, string message)> List(string userId, QueryOptions<Todo> queryOptions = null);
        Task<(ExecutionState executionState, IQueryable<Todo> entity, string message)> List(long userId, QueryOptions<Todo> queryOptions = null);

    }
}