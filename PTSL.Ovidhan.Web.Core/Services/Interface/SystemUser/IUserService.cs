using PTSL.Ovidhan.Web.Core.Helper.Enum;
using PTSL.Ovidhan.Web.Core.Model;

namespace PTSL.Ovidhan.Web.Core.Services.Interface.SystemUser
{
    public interface IUserService
	{
		(ExecutionState executionState, List<UserVM> entity, string message) List();
		(ExecutionState executionState, UserVM entity, string message) Create(UserRegisterModel model);
		(ExecutionState executionState, UserVM entity, string message) GetById(long? id);
		(ExecutionState executionState, UserVM entity, string message) GetByEmail(string email);
		(ExecutionState executionState, UserVM entity, string message) Update(UserVM model);
		(ExecutionState executionState, UserVM entity, string message) Delete(long? id);
		(ExecutionState executionState, LoginResultVM entity, string message) UserLogin(LoginVM model);
    }
}
