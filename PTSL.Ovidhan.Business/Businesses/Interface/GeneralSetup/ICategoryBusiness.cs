using System.Linq;
using System.Threading.Tasks;

using PTSL.Ovidhan.Business.BaseBusinesses;
using PTSL.Ovidhan.Common.Entity.GeneralSetup;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.QuerySerialize.Implementation;

namespace PTSL.Ovidhan.Business.Businesses.Interface.GeneralSetup
{
    public interface ICategoryBusiness : IBaseBusiness<Category>
    {
        Task<(ExecutionState executionState, IQueryable<Category> entity, string message)> List(string userId, QueryOptions<Category> queryOptions = null);
    }
}