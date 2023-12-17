using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using PTSL.Ovidhan.Business.BaseBusinesses;
using PTSL.Ovidhan.Business.Businesses.Interface.GeneralSetup;
using PTSL.Ovidhan.Common.Entity.Tasks;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.QuerySerialize.Implementation;
using PTSL.Ovidhan.DAL.UnitOfWork;

namespace PTSL.Ovidhan.Business.Businesses.Implementation.GeneralSetup
{
    public class TodoBusiness : BaseBusiness<Todo>, ITodoBusiness
    {
        private readonly ICategoryBusiness _categoryBusiness;
        public TodoBusiness(GENERICUnitOfWork unitOfWork, ICategoryBusiness categoryBusiness)
            : base(unitOfWork)
        {
            _categoryBusiness = categoryBusiness;
        }
        public override Task<(ExecutionState executionState, Todo entity, string message)> GetAsync(FilterOptions<Todo> filterOptions)
        {
            filterOptions = new FilterOptions<Todo>
            {
                FilterExpression = e => e.Id == e.Id,
                IncludeExpression = p => p.Include(e => e.Reminders)
            };
            return base.GetAsync(filterOptions);
        }

        public async Task<(ExecutionState executionState, Todo entity, string message)> GetTasksWithRemindersByUserId(string key)
        {

            FilterOptions<Todo> filterOptions = new FilterOptions<Todo>
            {
                FilterExpression = e => e.UserId == key,
                IncludeExpression = p => p.Include(e => e.Reminders)
            };
            return await base.GetAsync(filterOptions);
        }
        public async Task<(ExecutionState executionState, IQueryable<Todo> entity, string message)> List(string userId, QueryOptions<Todo> queryOptions = null)
        {
            queryOptions = new QueryOptions<Todo>
            {
                FilterExpression = f => f.UserId == userId,
                IncludeExpression = e => e.Include(a => a.Reminders)
            };
            return await base.List(queryOptions);
        }

        public async Task<(ExecutionState executionState, IQueryable<Todo> entity, string message)> List(long id, QueryOptions<Todo> queryOptions = null)
        {
            queryOptions = new QueryOptions<Todo>
            {
                FilterExpression = f => f.CategoryId == id,
                IncludeExpression = e => e.Include(a => a.Reminders)
            };
            return await base.List(queryOptions);
        }
    }
}