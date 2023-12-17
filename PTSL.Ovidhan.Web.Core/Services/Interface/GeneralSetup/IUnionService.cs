using PTSL.Ovidhan.Web.Core.Helper.Enum;
using PTSL.Ovidhan.Web.Core.Model.GeneralSetup;

namespace PTSL.Ovidhan.Web.Core.Services.Interface.GeneralSetup
{
    public interface IUnionService
    {
        (ExecutionState executionState, List<UnionVM> entity, string message) List();
        (ExecutionState executionState, UnionVM entity, string message) Create(UnionVM model);
        (ExecutionState executionState, UnionVM entity, string message) GetById(long? id);
        (ExecutionState executionState, UnionVM entity, string message) Update(UnionVM model);
        (ExecutionState executionState, UnionVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
        (ExecutionState executionState, List<UnionVM> entity, string message) ListByUpazilla(long UpazillaId);
        (ExecutionState executionState, List<UnionVM> entity, string message) ListByMultipleUpazilla(List<long> upazillas);
    }
}
