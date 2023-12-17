using PTSL.Ovidhan.Web.Core.Helper.Enum;
using PTSL.Ovidhan.Web.Core.Model.GeneralSetup;

namespace PTSL.Ovidhan.Web.Core.Services.Interface.GeneralSetup
{
    public interface IUpazillaService
    {
        (ExecutionState executionState, List<UpazillaVM> entity, string message) List();
        (ExecutionState executionState, UpazillaVM entity, string message) Create(UpazillaVM model);
        (ExecutionState executionState, UpazillaVM entity, string message) GetById(long? id);
        (ExecutionState executionState, UpazillaVM entity, string message) Update(UpazillaVM model);
        (ExecutionState executionState, UpazillaVM entity, string message) Delete(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
        (ExecutionState executionState, List<UpazillaVM> entity, string message) ListByDistrict(long districtId);
    }
}
