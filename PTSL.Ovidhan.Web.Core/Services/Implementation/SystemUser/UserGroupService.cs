using Newtonsoft.Json;
using PTSL.Ovidhan.Web.Core.Helper;
using PTSL.Ovidhan.Web.Core.Helper.Enum;
using PTSL.Ovidhan.Web.Core.Model.ApiResponseModel;
using PTSL.Ovidhan.Web.Core.Model.EntityViewModels.SystemUser;
using PTSL.Ovidhan.Web.Core.Services.Interface.SystemUser;
using PTSL.Ovidhan.Web.Helper;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace PTSL.Ovidhan.Web.Core.Services.Implementation.SystemUser
{
    public class UserGroupService : IUserGroupService
    {
        private readonly HttpHelper httpHelper;

        public UserGroupService(HttpHelper httpHelper)
        {
            this.httpHelper = httpHelper;
        }

        public (ExecutionState executionState, List<UserGroupVM> entity, string message) List()
        {
            (ExecutionState executionState, List<UserGroupVM> entity, string message) returnResponse;
            try
            {
                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.UserGroupList));
                var json = httpHelper.Get(URL);
                WebApiResponse<List<UserGroupVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<UserGroupVM>>>(json);
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
        public (ExecutionState executionState, UserGroupVM entity, string message) Create(UserGroupVM model)
        {
            (ExecutionState executionState, UserGroupVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.UserGroup));
                var json = httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<UserGroupVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UserGroupVM>>(json);
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
        public (ExecutionState executionState, UserGroupVM entity, string message) GetById(long? id)
        {
            (ExecutionState executionState, UserGroupVM entity, string message) returnResponse;
            try
            {
                UserGroupVM model = new UserGroupVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.UserGroup + "/" + id));
                var json = httpHelper.Get(URL);
                WebApiResponse<UserGroupVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UserGroupVM>>(json);
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
        public (ExecutionState executionState, UserGroupVM entity, string message) Update(UserGroupVM model)
        {
            (ExecutionState executionState, UserGroupVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.UserGroup));
                var json = httpHelper.Put(URL, respJson, "application/json");
                WebApiResponse<UserGroupVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UserGroupVM>>(json);
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
        public (ExecutionState executionState, UserGroupVM entity, string message) Delete(long? id)
        {
            (ExecutionState executionState, UserGroupVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, UserGroupVM entity, string message) IsExist = GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.UserGroup));
                    var json = httpHelper.Put(URL, respJson, "application/json");
                    WebApiResponse<UserGroupVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UserGroupVM>>(json);
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
    }
}