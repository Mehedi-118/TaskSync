using Microsoft.AspNetCore.Http;
using PTSL.Ovidhan.Web.Core.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PTSL.Ovidhan.Web.Helper
{
	public class HttpHelper
    {
		private readonly IHttpContextAccessor _contextAccessor;

		public HttpHelper(IHttpContextAccessor contextAccessor)
        {
			_contextAccessor = contextAccessor;
		}

        public string Get(string uri)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                request.Headers.Add("Authorization", "Bearer " + MySession.Current(_contextAccessor.HttpContext).Token);
                request.Headers.Add("UserId", MySession.Current(_contextAccessor.HttpContext).UserId.ToString());
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<string> GetAsync(string uri)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                request.Headers.Add("Authorization", "Bearer " + MySession.Current(_contextAccessor.HttpContext).Token);
                request.Headers.Add("UserId", MySession.Current(_contextAccessor.HttpContext).UserId.ToString());
                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return await reader.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Post(string uri, string data, string contentType, string method = "POST")
        {
            try
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                request.Headers.Add("Authorization", "Bearer " + MySession.Current(_contextAccessor.HttpContext).Token);
                request.Headers.Add("UserId", MySession.Current(_contextAccessor.HttpContext).UserId.ToString());
                request.ContentLength = dataBytes.Length;
                request.ContentType = contentType;
                request.Method = method;
                using (Stream requestBody = request.GetRequestStream())
                {
                    requestBody.Write(dataBytes, 0, dataBytes.Length);
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> PostAsync(string uri, string data, string contentType, string method = "POST")
        {
            try
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                request.Headers.Add("Authorization", "Bearer " + MySession.Current(_contextAccessor.HttpContext).Token);
                request.Headers.Add("UserId", MySession.Current(_contextAccessor.HttpContext).UserId.ToString());
                request.ContentLength = dataBytes.Length;
                request.ContentType = contentType;
                request.Method = method;
                using (Stream requestBody = request.GetRequestStream())
                {
                    await requestBody.WriteAsync(dataBytes, 0, dataBytes.Length);
                }
                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return await reader.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Put(string uri, string data, string contentType, string method = "PUT")
        {
            try
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                request.Headers.Add("Authorization", "Bearer " + MySession.Current(_contextAccessor.HttpContext).Token);
                request.Headers.Add("UserId", MySession.Current(_contextAccessor.HttpContext).UserId.ToString());
                request.ContentLength = dataBytes.Length;
                request.ContentType = contentType;
                request.Method = method;
                using (Stream requestBody = request.GetRequestStream())
                {
                    requestBody.Write(dataBytes, 0, dataBytes.Length);
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> PutAsync(string uri, string data, string contentType, string method = "PUT")
        {
            try
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                request.Headers.Add("Authorization", "Bearer " + MySession.Current(_contextAccessor.HttpContext).Token);
                request.Headers.Add("UserId", MySession.Current(_contextAccessor.HttpContext).UserId.ToString());
                request.ContentLength = dataBytes.Length;
                request.ContentType = contentType;
                request.Method = method;
                using (Stream requestBody = request.GetRequestStream())
                {
                    await requestBody.WriteAsync(dataBytes, 0, dataBytes.Length);
                }
                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return await reader.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






        public string Delete(string uri, string data, string contentType, string method = "DELETE")
        {
            try
            {
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                request.Headers.Add("Authorization", "Bearer " + MySession.Current(_contextAccessor.HttpContext).Token);
                request.Headers.Add("UserId", MySession.Current(_contextAccessor.HttpContext).UserId.ToString());
                request.ContentLength = dataBytes.Length;
                request.ContentType = contentType;
                request.Method = method;
                using (Stream requestBody = request.GetRequestStream())
                {
                    requestBody.Write(dataBytes, 0, dataBytes.Length);
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
