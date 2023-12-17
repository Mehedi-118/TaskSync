using PTSL.Ovidhan.Web.Core.Helper.Enum;
using PTSL.Ovidhan.Web.Core.Model.EntityViewModels.SystemUser;
using System.Collections.Generic;

namespace PTSL.Ovidhan.Web.Core.Services.Interface.SystemUser
{
	public interface IAccesslistService
	{
		(ExecutionState executionState, List<AccesslistVM> entity, string message) List();
		(ExecutionState executionState, AccesslistVM entity, string message) Create(AccesslistVM model);
		(ExecutionState executionState, AccesslistVM entity, string message) GetById(long? id);
		(ExecutionState executionState, AccesslistVM entity, string message) Update(AccesslistVM model);
		(ExecutionState executionState, AccesslistVM entity, string message) Delete(long? id);
	}
}
