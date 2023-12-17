using Newtonsoft.Json;

using PTSL.Ovidhan.Web.Core.Helper;
using PTSL.Ovidhan.Web.Core.Helper.Enum;
using PTSL.Ovidhan.Web.Core.Model;
using PTSL.Ovidhan.Web.Core.Model.ApiResponseModel;
using PTSL.Ovidhan.Web.Core.Model.EntityViewModels.SystemUser;
using PTSL.Ovidhan.Web.Core.Services.Interface.SystemUser;
using PTSL.Ovidhan.Web.Helper;

namespace PTSL.Ovidhan.Web.Core.Services.Implementation.SystemUser
{
    public class UserService : IUserService
    {
        private readonly HttpHelper httpHelper;

        public UserService(HttpHelper httpHelper)
        {
            this.httpHelper = httpHelper;
        }

        public (ExecutionState executionState, List<UserVM> entity, string message) List()
        {
            (ExecutionState executionState, List<UserVM> entity, string message) returnResponse;
            try
            {
                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.UserList));
                var json = httpHelper.Get(URL);
                WebApiResponse<List<UserVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<UserVM>>>(json);
                returnResponse.executionState = responseJson.ExecutionState;
                returnResponse.entity = responseJson.Data;
                returnResponse.message = responseJson.Message;
            }
            catch (Exception ex)
            {
                returnResponse.executionState = ExecutionState.Failure;
                returnResponse.entity = null;
                returnResponse.message = ex.Message.ToString();
            }
            return returnResponse;
        }
        public (ExecutionState executionState, UserVM entity, string message) Create(UserRegisterModel model)
        {
            (ExecutionState executionState, UserVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.User));
                var json = httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<UserVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UserVM>>(json);
                returnResponse.executionState = responseJson.ExecutionState;
                returnResponse.entity = responseJson.Data;
                returnResponse.message = responseJson.Message;
            }
            catch (Exception ex)
            {
                returnResponse.executionState = ExecutionState.Failure;
                returnResponse.entity = null;
                returnResponse.message = ex.Message.ToString();
            }
            return returnResponse;
        }

        public (ExecutionState executionState, UserVM entity, string message) GetById(long? id)
        {
            (ExecutionState executionState, UserVM entity, string message) returnResponse;
            try
            {
                UserVM model = new UserVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.User + "/" + id));
                var json = httpHelper.Get(URL);
                WebApiResponse<UserVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UserVM>>(json);
                returnResponse.executionState = responseJson.ExecutionState;
                returnResponse.entity = responseJson.Data;
                returnResponse.message = responseJson.Message;
            }
            catch (Exception ex)
            {
                returnResponse.executionState = ExecutionState.Failure;
                returnResponse.entity = null;
                returnResponse.message = ex.Message.ToString();
            }
            return returnResponse;
        }
        public (ExecutionState executionState, UserVM entity, string message) Update(UserVM model)
        {
            (ExecutionState executionState, UserVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.User));
                var json = httpHelper.Put(URL, respJson, "application/json");
                WebApiResponse<UserVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UserVM>>(json);
                returnResponse.executionState = responseJson.ExecutionState;
                returnResponse.entity = responseJson.Data;
                returnResponse.message = responseJson.Message;
            }
            catch (Exception ex)
            {
                returnResponse.executionState = ExecutionState.Failure;
                returnResponse.entity = null;
                returnResponse.message = ex.Message.ToString();
            }
            return returnResponse;
        }
        public (ExecutionState executionState, UserVM entity, string message) Delete(long? id)
        {
            (ExecutionState executionState, UserVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, UserVM entity, string message) IsExist = GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.User));
                    var json = httpHelper.Put(URL, respJson, "application/json");
                    WebApiResponse<UserVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UserVM>>(json);
                    returnResponse.executionState = responseJson.ExecutionState;
                    returnResponse.entity = responseJson.Data;
                    returnResponse.message = responseJson.Message;
                }
                else
                {
                    returnResponse.executionState = ExecutionState.Failure;
                    returnResponse.entity = null;
                    returnResponse.message = "This color is not exist.";
                }

            }
            catch (Exception ex)
            {
                returnResponse.executionState = ExecutionState.Failure;
                returnResponse.entity = null;
                returnResponse.message = ex.Message.ToString();
            }
            return returnResponse;
        }

        public (ExecutionState executionState, LoginResultVM entity, string message) UserLogin(LoginVM model)
        {
            (ExecutionState executionState, LoginResultVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.UserLogin));
                var json = httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<LoginResultVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<LoginResultVM>>(json);
                returnResponse.executionState = responseJson.ExecutionState;
                returnResponse.entity = responseJson.Data;
                returnResponse.message = responseJson.Message;
            }
            catch (Exception ex)
            {
                returnResponse.executionState = ExecutionState.Failure;
                returnResponse.entity = null;
                returnResponse.message = ex.Message.ToString();
            }
            return returnResponse;
        }

        public (ExecutionState executionState, UserVM entity, string message) GetByEmail(string email)
        {
            (ExecutionState executionState, UserVM entity, string message) returnResponse;
            try
            {
                UserVM model = new UserVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.User + "/" + email));
                var json = httpHelper.Get(URL);
                WebApiResponse<UserVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UserVM>>(json);
                returnResponse.executionState = responseJson.ExecutionState;
                returnResponse.entity = responseJson.Data;
                returnResponse.message = responseJson.Message;
            }
            catch (Exception ex)
            {
                returnResponse.executionState = ExecutionState.Failure;
                returnResponse.entity = null;
                returnResponse.message = ex.Message.ToString();
            }
            return returnResponse;
        }
    }
}