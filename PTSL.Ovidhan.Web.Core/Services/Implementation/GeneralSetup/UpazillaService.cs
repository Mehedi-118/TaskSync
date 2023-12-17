using Newtonsoft.Json;

using PTSL.Ovidhan.Web.Core.Helper;
using PTSL.Ovidhan.Web.Core.Helper.Enum;
using PTSL.Ovidhan.Web.Core.Model.ApiResponseModel;
using PTSL.Ovidhan.Web.Core.Model.GeneralSetup;
using PTSL.Ovidhan.Web.Core.Services.Interface.GeneralSetup;
using PTSL.Ovidhan.Web.Helper;

namespace PTSL.Ovidhan.Web.Services.Implementation.GeneralSetup
{
    public class UpazillaService : IUpazillaService
    {
        private readonly HttpHelper _httpHelper;

        public UpazillaService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public (ExecutionState executionState, List<UpazillaVM> entity, string message) List()
        {
            (ExecutionState executionState, List<UpazillaVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.UpazillaList));
                var json = _httpHelper.Get(URL);
                WebApiResponse<List<UpazillaVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<UpazillaVM>>>(json);
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

        public (ExecutionState executionState, List<UpazillaVM> entity, string message) ListByDistrict(long districtId)
        {
            (ExecutionState executionState, List<UpazillaVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"{URLHelper.Upazilla}/ListByDistrict/{districtId}"));
                var json = _httpHelper.Get(URL);
                WebApiResponse<List<UpazillaVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<UpazillaVM>>>(json);
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

        public (ExecutionState executionState, UpazillaVM entity, string message) Create(UpazillaVM model)
        {
            (ExecutionState executionState, UpazillaVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.Upazilla));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<UpazillaVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UpazillaVM>>(json);
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

        public (ExecutionState executionState, UpazillaVM entity, string message) GetById(long? id)
        {
            (ExecutionState executionState, UpazillaVM entity, string message) returnResponse;
            try
            {
                UpazillaVM model = new UpazillaVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.Upazilla + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<UpazillaVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UpazillaVM>>(json);
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
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.UpazillaDoesExist + "/" + id));
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

        public (ExecutionState executionState, UpazillaVM entity, string message) Update(UpazillaVM model)
        {
            (ExecutionState executionState, UpazillaVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.Upazilla));
                var json = _httpHelper.Put(URL, respJson, "application/json");
                WebApiResponse<UpazillaVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UpazillaVM>>(json);
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

        public (ExecutionState executionState, UpazillaVM entity, string message) Delete(long? id)
        {
            (ExecutionState executionState, UpazillaVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, UpazillaVM entity, string message) IsExist = GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.Upazilla));
                    var json = _httpHelper.Put(URL, respJson, "application/json");
                    WebApiResponse<UpazillaVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UpazillaVM>>(json);
                    returnResponse.executionState = responseJson.ExecutionState;
                    returnResponse.entity = responseJson.Data;
                    returnResponse.message = responseJson.Message;
                }
                else
                {
                    returnResponse.executionState = ExecutionState.Failure;
                    returnResponse.entity = null;
                    returnResponse.message = "This item does not exist.";
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