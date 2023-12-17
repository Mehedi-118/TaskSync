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
    public class AccessMapperService : IAccessMapperService
    {
        private readonly HttpHelper httpHelper;

        public AccessMapperService(HttpHelper httpHelper)
        {
            this.httpHelper = httpHelper;
        }

        public (ExecutionState executionState, List<AccessMapperVM> entity, string message) List()
        {
            (ExecutionState executionState, List<AccessMapperVM> entity, string message) returnResponse;
            try
            {
                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.AccessMapperList));
                var json = httpHelper.Get(URL);
                WebApiResponse<List<AccessMapperVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<AccessMapperVM>>>(json);
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
        public (ExecutionState executionState, AccessMapperVM entity, string message) Create(AccessMapperVM model)
        {
            (ExecutionState executionState, AccessMapperVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.AccessMapper));
                var json = httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<AccessMapperVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<AccessMapperVM>>(json);
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
        public (ExecutionState executionState, AccessMapperVM entity, string message) GetById(long? id)
        {
            (ExecutionState executionState, AccessMapperVM entity, string message) returnResponse;
            try
            {
                AccessMapperVM model = new AccessMapperVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.AccessMapper + "/" + id));
                var json = httpHelper.Get(URL);
                WebApiResponse<AccessMapperVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<AccessMapperVM>>(json);
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
        public (ExecutionState executionState, AccessMapperVM entity, string message) Update(AccessMapperVM model)
        {
            (ExecutionState executionState, AccessMapperVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.AccessMapper));
                var json = httpHelper.Put(URL, respJson, "application/json");
                WebApiResponse<AccessMapperVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<AccessMapperVM>>(json);
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
        public (ExecutionState executionState, AccessMapperVM entity, string message) Delete(long? id)
        {
            (ExecutionState executionState, AccessMapperVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, AccessMapperVM entity, string message) IsExist = GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.AccessMapper));
                    var json = httpHelper.Put(URL, respJson, "application/json");
                    WebApiResponse<AccessMapperVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<AccessMapperVM>>(json);
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