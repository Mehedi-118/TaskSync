using Newtonsoft.Json;

using PTSL.Ovidhan.Web.Core.Helper;
using PTSL.Ovidhan.Web.Core.Helper.Enum;
using PTSL.Ovidhan.Web.Core.Model.ApiResponseModel;
using PTSL.Ovidhan.Web.Core.Model.GeneralSetup;
using PTSL.Ovidhan.Web.Core.Services.Interface.GeneralSetup;
using PTSL.Ovidhan.Web.Helper;

namespace PTSL.Ovidhan.Web.Core.Services.Implementation.GeneralSetup
{
    public class UnionService : IUnionService
    {
        private readonly HttpHelper _httpHelper;

        public UnionService(HttpHelper httpHelper)
        {
            _httpHelper = httpHelper;
        }

        public (ExecutionState executionState, List<UnionVM> entity, string message) List()
        {
            (ExecutionState executionState, List<UnionVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.UnionList));
                var json = _httpHelper.Get(URL);
                WebApiResponse<List<UnionVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<UnionVM>>>(json);
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

        public (ExecutionState executionState, List<UnionVM> entity, string message) ListByMultipleUpazilla(List<long> upazillas)
        {
            (ExecutionState executionState, List<UnionVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(upazillas);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.UnionListByMultipleUpazilla));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<List<UnionVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<UnionVM>>>(json);
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

        public (ExecutionState executionState, List<UnionVM> entity, string message) ListByUpazilla(long UpazillaId)
        {
            (ExecutionState executionState, List<UnionVM> entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(null);

                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format($"{URLHelper.Union}/ListByUpazilla/{UpazillaId}"));
                var json = _httpHelper.Get(URL);
                WebApiResponse<List<UnionVM>> responseJson = JsonConvert.DeserializeObject<WebApiResponse<List<UnionVM>>>(json);
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

        public (ExecutionState executionState, UnionVM entity, string message) Create(UnionVM model)
        {
            (ExecutionState executionState, UnionVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.Union));
                var json = _httpHelper.Post(URL, respJson, "application/json");
                WebApiResponse<UnionVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UnionVM>>(json);
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

        public (ExecutionState executionState, UnionVM entity, string message) GetById(long? id)
        {
            (ExecutionState executionState, UnionVM entity, string message) returnResponse;
            try
            {
                UnionVM model = new UnionVM();
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.Union + "/" + id));
                var json = _httpHelper.Get(URL);
                WebApiResponse<UnionVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UnionVM>>(json);
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
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.UnionDoesExist + "/" + id));
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

        public (ExecutionState executionState, UnionVM entity, string message) Update(UnionVM model)
        {
            (ExecutionState executionState, UnionVM entity, string message) returnResponse;
            try
            {
                var respJson = JsonConvert.SerializeObject(model);
                var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.Union));
                var json = _httpHelper.Put(URL, respJson, "application/json");
                WebApiResponse<UnionVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UnionVM>>(json);
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

        public (ExecutionState executionState, UnionVM entity, string message) Delete(long? id)
        {
            (ExecutionState executionState, UnionVM entity, string message) returnResponse;
            try
            {
                (ExecutionState executionState, UnionVM entity, string message) IsExist = GetById(id);
                if (IsExist.entity != null)
                {
                    IsExist.entity.IsDeleted = true;
                    IsExist.entity.DeletedAt = DateTime.Now;
                    var respJson = JsonConvert.SerializeObject(IsExist.entity);
                    var URL = String.Concat(URLHelper.ApiBaseURL, string.Format(URLHelper.Union));
                    var json = _httpHelper.Put(URL, respJson, "application/json");
                    WebApiResponse<UnionVM> responseJson = JsonConvert.DeserializeObject<WebApiResponse<UnionVM>>(json);
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