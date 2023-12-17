using PTSL.Ovidhan.Web.Core.Helper.Enum;

namespace PTSL.Ovidhan.Web.Core.Model.ApiResponseModel
{
	public class WebApiResponse<T>
	{
		public WebApiResponse()
		{ }

		public WebApiResponse((ExecutionState executionState, T entity, string message) result)
		{
			ExecutionState = result.executionState;
			Data = result.entity;
			Message = result.message;
		}

		public WebApiResponse((ExecutionState executionState, string message) result)
		{
			ExecutionState = result.executionState;
			Message = result.message;
		}

		//[JsonProperty("ExecutionState")]
		public ExecutionState ExecutionState { get; set; }

		//[JsonProperty("Message")]
		public string Message { get; set; }

		//[JsonProperty("Data")]
		public T Data { get; set; }
	}
}
