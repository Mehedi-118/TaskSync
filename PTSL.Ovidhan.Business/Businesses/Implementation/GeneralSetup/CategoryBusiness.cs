using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using PTSL.Ovidhan.Business.BaseBusinesses;
using PTSL.Ovidhan.Business.Businesses.Interface.GeneralSetup;
using PTSL.Ovidhan.Common.Entity.GeneralSetup;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.Ovidhan.Common.QuerySerialize.Implementation;
using PTSL.Ovidhan.DAL.UnitOfWork;

namespace PTSL.Ovidhan.Business.Businesses.Implementation.GeneralSetup
{
    public class CategoryBusiness : BaseBusiness<Category>, ICategoryBusiness
    {
        public CategoryBusiness(GENERICUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
        public async Task<(ExecutionState executionState, IQueryable<Category> entity, string message)> List(string userId, QueryOptions<Category> queryOptions = null)
        {
            queryOptions = new QueryOptions<Category>
            {
                FilterExpression = e => e.UserId == userId,
                IncludeExpression = r => r.Include(p => p.Todos)
            };
            return await base.List(queryOptions);
        }
    }
}