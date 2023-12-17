using PTSL.Ovidhan.Web.Core.Helper.Enum;
using PTSL.Ovidhan.Web.Core.Model.EntityViewModels.SystemUser;
using System.Collections.Generic;

namespace PTSL.Ovidhan.Web.Core.Services.Interface.SystemUser
{
	public interface IUserGroupService
	{
		(ExecutionState executionState, List<UserGroupVM> entity, string message) List();
		(ExecutionState executionState, UserGroupVM entity, string message) Create(UserGroupVM model);
		(ExecutionState executionState, UserGroupVM entity, string message) GetById(long? id);
		(ExecutionState executionState, UserGroupVM entity, string message) Update(UserGroupVM model);
		(ExecutionState executionState, UserGroupVM entity, string message) Delete(long? id);
	}
}
