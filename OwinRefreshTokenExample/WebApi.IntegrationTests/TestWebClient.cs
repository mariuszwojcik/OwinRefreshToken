using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;

namespace WebApi.IntegrationTests
{
    public class TestWebClient : WebClient
    {
        private const string WebApiUrl = @"http://localhost:6098/";
        private const string TokenEndpointUrl = @"https://localhost:44301/token";

        public HttpStatusCode LastOperationHttpStatusCode { get; private set; }

        public string LastOperationResponse { get; private set; }

        private string LoadData(string path)
        {
            try
            {
                LastOperationResponse = DownloadString(WebApiUrl + path);
                LastOperationHttpStatusCode = HttpStatusCode.OK;
                return LastOperationResponse;
            }
            catch (WebException e)
            {
                var response = (HttpWebResponse) e.Response;
                LastOperationHttpStatusCode = response.StatusCode;
                LastOperationResponse = string.Empty;
                return string.Empty;
            }
        }

        public string LoadUnprotectedData()
        {
            return LoadData("SecurityTest/unprotected");
        }

        public string LoadProtectedData()
        {
            return LoadData("SecurityTest/protected");
        }

        public string Login(string username, string password)
        {
            try
            {
                var data = new NameValueCollection
                {
                    {"grant_type", "password"},
                    {"username", username},
                    {"password", password}
                };
                Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var result = UploadValues(TokenEndpointUrl, data);
                LastOperationHttpStatusCode = HttpStatusCode.OK;
                LastOperationResponse = System.Text.Encoding.Default.GetString(result);
                return LastOperationResponse;
            }
            catch (WebException e)
            {
                var response = (HttpWebResponse)e.Response;
                LastOperationHttpStatusCode = response.StatusCode;
                using (var s = response.GetResponseStream())
                using (var sr = new StreamReader(s))
                {
                    LastOperationResponse = sr.ReadToEnd();
                    Console.WriteLine(LastOperationResponse);
                }
                return string.Empty;
            }

            
        }
    }
}