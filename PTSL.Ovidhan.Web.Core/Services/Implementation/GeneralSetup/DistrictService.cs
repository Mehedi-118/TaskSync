using Newtonsoft.Json;

using PTSL.Ovidhan.Web.Core.Helper;
using PTSL.Ovidhan.Web.Core.Helper.Enum;
using PTSL.Ovidhan.Web.Core.Model.ApiResponseModel;
using PTSL.Ovidhan.Web.Core.Model.GeneralSetup;
using PTSL.Ovidhan.Web.Core.Services.Interface.GeneralSetup;
using PTSL.Ovidhan.Web.Helper;

namespace PTSL.Ovidhan.Web.Core.Services.Implementation.GeneralSetup
{
    public class DistrictService : IDistrictService
    {
        private readonly HttpHelper _httpHelper;

        public DistrictService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public (ExecutionState executionState, List<DistrictVM> entity, string message) List()
        {
            (ExecutionState executionState, List<DistrictVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.DistrictList));
                var json = _httpHelper.Get(URL);
                WebApiResponse<List<DistrictVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<DistrictVM>>>(json);
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
        public (ExecutionState executionState, List<DistrictVM> entity, string message) ListByDivision(long divisionId)
        {
            (ExecutionState executionState, List<DistrictVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"{URLHelper.District}/ListByDivision/{divisionId}"));
                var json = _httpHelper.Get(URL);
                WebApiResponse<List<DistrictVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<DistrictVM>>>(json);
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
        public (ExecutionState executionState, DistrictVM entity, string message) Create(DistrictVM model)
        {
            (ExecutionState executionState, DistrictVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.District));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<DistrictVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<DistrictVM>>(json);
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
        public (ExecutionState executionState, DistrictVM entity, string message) GetById(long? id)
        {
            (ExecutionState executionState, DistrictVM entity, string message) returnResponse;
            try
            {
                DistrictVM model = new DistrictVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.District + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<DistrictVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<DistrictVM>>(json);
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

        public (ExecutionState executionState, List<DistrictVM> entity, string message) GetDistrictByDivisionId(long? id)
        {
            (ExecutionState executionState, List<DistrictVM> entity, string message) returnResponse;
            try
            {
                DistrictVM model = new DistrictVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.District + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<List<DistrictVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<DistrictVM>>>(json);
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

        public (ExecutionState executionState, string message) DoesExist(long? id)
        {
            (ExecutionState executionState, string message) returnResponse;
            try
            {
                DegreeVM model = new DegreeVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.DistrictDoesExist + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<DegreeVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<DegreeVM>>(json);
                returnResponse.executionState = responseJson.ExecutionState;
                returnResponse.message = responseJson.Message;
            }
            catch (Exception ex)
            {
                returnResponse.executionState = ExecutionState.Failure;
                returnResponse.message = ex.Message.ToString();
            }
            return returnResponse;
        }
        public (ExecutionState executionState, DistrictVM entity, string message) Update(DistrictVM model)
        {
            (ExecutionState executionState, DistrictVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.District));
                var json = _httpHelper.Put(URL, respJson, "application/json");
                WebApiResponse<DistrictVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<DistrictVM>>(json);
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
        public (ExecutionState executionState, DistrictVM entity, string message) Delete(long? id)
        {
            (ExecutionState executionState, DistrictVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, DistrictVM entity, string message) IsExist = GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.District));
                    var json = _httpHelper.Put(URL, respJson, "application/json");
                    WebApiResponse<DistrictVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<DistrictVM>>(json);
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