using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text.Json;

namespace PTSL.Ovidhan.Web.Core.Helper
{
	public class MySession
	{
		// private constructor
		private MySession()
		{
			Token = "";
		}

		// Gets the current session.
		//public MySession Current
		//{
		//	get
		//	{
		//		//MySession session =
		//		//  (MySession)_httpContext.HttpContext.Session.Get("__MySession__");

		//		var session = _httpContext.HttpContext.Session.GetString("__MySession__");

		//		if (session == null)
		//		{
		//			session = new MySession();
		//			HttpContext.Current.Session["__MySession__"] = session;
		//		}
		//		return JsonSerializer.Deserialize<MySession>(session);
		//	}
		//}

		public static MySession Current(HttpContext? httpContext)
		{
			ArgumentNullException.ThrowIfNull(httpContext, nameof(httpContext));

            var token = httpContext.Session.GetString("Token");
            var userEmail = httpContext.Session.GetString("UserEmail");
            _ = long.TryParse(httpContext.Session.GetString("UserId"), out var userId);

            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userEmail) || userId == 0)
			{
				return new MySession();
			}

			return new MySession() { Token = token, UserEmail = userEmail, UserId = userId };
		}

		// **** add your session properties here, e.g like this:
		public string Token { get; set; }
		public long UserId { get; set; }
		public string UserEmail { get; set; }
	}
}