using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using PTSL.Ovidhan.Common.Entity.GeneralSetup;
using PTSL.Ovidhan.Common.Enum;
using PTSL.Ovidhan.Common.Model.EntityViewModels.GeneralSetup;
using PTSL.Ovidhan.Common.QuerySerialize.Implementation;
using PTSL.Ovidhan.Service.BaseServices;

namespace PTSL.Ovidhan.Service.Services.Interface.GeneralSetup
{
    public interface ICategoryService : IBaseService<CategoryVM, Category>
    {
        Task<(ExecutionState executionState, IList<CategoryVM> entity, string message)> CreateInitialCategoryAsync(string  userId);
        Task<(ExecutionState executionState, IList<CategoryVM> entity, string message)> List(string userId, QueryOptions<Category> queryOptions = null);

    }
}