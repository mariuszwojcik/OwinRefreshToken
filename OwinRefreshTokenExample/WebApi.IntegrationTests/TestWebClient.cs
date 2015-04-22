using System.Net;

namespace WebApi.IntegrationTests
{
    public class TestWebClient : WebClient
    {
        private const string WebApiUrl = @"http://localhost:6098/";

        public HttpStatusCode LastOperationHttpStatusCode { get; private set; }

        private string LoadData(string path)
        {
            try
            {
                var result = DownloadString(WebApiUrl + path);
                LastOperationHttpStatusCode = HttpStatusCode.OK;
                return result;
            }
            catch (WebException e)
            {
                var response = (HttpWebResponse) e.Response;
                LastOperationHttpStatusCode = response.StatusCode;
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
    }
}