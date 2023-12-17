using PTSL.Ovidhan.Web.Core.Helper.Enum;
using PTSL.Ovidhan.Web.Core.Model.GeneralSetup;

namespace PTSL.Ovidhan.Web.Core.Services.Interface.GeneralSetup
{
    public interface IDistrictService
    {
        (ExecutionState executionState, List<DistrictVM> entity, string message) List();
        (ExecutionState executionState, DistrictVM entity, string message) Create(DistrictVM model);
        (ExecutionState executionState, DistrictVM entity, string message) GetById(long? id);
        (ExecutionState executionState, DistrictVM entity, string message) Update(DistrictVM model);
        (ExecutionState executionState, DistrictVM entity, string message) Delete(long? id);
        (ExecutionState executionState, List<DistrictVM> entity, string message) GetDistrictByDivisionId(long? id);
        (ExecutionState executionState, string message) DoesExist(long? id);
        (ExecutionState executionState, List<DistrictVM> entity, string message) ListByDivision(long divisionId);
    }
}
