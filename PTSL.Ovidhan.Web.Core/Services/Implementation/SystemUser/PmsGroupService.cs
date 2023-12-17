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
    public class PmsGroupService : IPmsGroupService
    {
        private readonly HttpHelper httpHelper;

        public PmsGroupService(HttpHelper httpHelper)
        {
            this.httpHelper = httpHelper;
        }

        public (ExecutionState executionState, List<PmsGroupVM> entity, string message) List()
        {
            (ExecutionState executionState, List<PmsGroupVM> entity, string message) returnResponse;
            try
            {
                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.PmsGroupList));
                var json = httpHelper.Get(URL);
                WebApiResponse<List<PmsGroupVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<PmsGroupVM>>>(json);
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
        public (ExecutionState executionState, PmsGroupVM entity, string message) Create(PmsGroupVM model)
        {
            (ExecutionState executionState, PmsGroupVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.PmsGroup));
                var json = httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<PmsGroupVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<PmsGroupVM>>(json);
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
        public (ExecutionState executionState, PmsGroupVM entity, string message) GetById(long? id)
        {
            (ExecutionState executionState, PmsGroupVM entity, string message) returnResponse;
            try
            {
                PmsGroupVM model = new PmsGroupVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.PmsGroup + "/" + id));
                var json = httpHelper.Get(URL);
                WebApiResponse<PmsGroupVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<PmsGroupVM>>(json);
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
        public (ExecutionState executionState, PmsGroupVM entity, string message) Update(PmsGroupVM model)
        {
            (ExecutionState executionState, PmsGroupVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.PmsGroup));
                var json = httpHelper.Put(URL, respJson, "application/json");
                WebApiResponse<PmsGroupVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<PmsGroupVM>>(json);
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
        public (ExecutionState executionState, PmsGroupVM entity, string message) Delete(long? id)
        {
            (ExecutionState executionState, PmsGroupVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, PmsGroupVM entity, string message) IsExist = GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = string.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.PmsGroup));
                    var json = httpHelper.Put(URL, respJson, "application/json");
                    WebApiResponse<PmsGroupVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<PmsGroupVM>>(json);
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