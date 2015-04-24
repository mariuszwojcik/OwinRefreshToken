using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace WebApi.IntegrationTests
{
    public class TestWebClient : WebClient
    {
        private const string WebApiUrl = @"http://localhost:6098/";
        private const string TokenEndpointUrl = @"https://localhost:44301/token";
        private OAuthToken _token;

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

        public string LoadSuperProtectedData()
        {
            return LoadData("SecurityTest/superprotected");
        }

        public string LoadSecret()
        {
            return LoadData("SecurityTest/secret");
        }

        public OAuthToken Login(string username, string password)
        {
            try
            {
                var data = new NameValueCollection
                {
                    {"grant_type", "password"},
                    {"username", username},
                    {"password", password},
                    {"client_id", "OwinRefreshTokenExample"}
                };
                Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var result = UploadValues(TokenEndpointUrl, data);
                LastOperationHttpStatusCode = HttpStatusCode.OK;
                LastOperationResponse = System.Text.Encoding.Default.GetString(result);

                var responseSerializer = new DataContractJsonSerializer(typeof(OAuthToken));

                using (var ms = new MemoryStream(result))
                {
                    _token = (OAuthToken) responseSerializer.ReadObject(ms);
                    Headers[HttpRequestHeader.Authorization] = "Bearer " + _token.access_token;
                }

                return _token;
            }
            catch (WebException e)
            {
                var response = (HttpWebResponse)e.Response;
                LastOperationHttpStatusCode = response.StatusCode;
                using (var s = response.GetResponseStream())
                using (var sr = new StreamReader(s))
                {
                    LastOperationResponse = sr.ReadToEnd();
                }
                return null;
            }
        }

        public string GetRoles(string email)
        {
            var address = String.Format("{0}/api/Account/Roles?email={1}", WebApiUrl, email);
            return DownloadString(address);
        }

        public void AddRole(string email, string role)
        {
            var address = String.Format("{0}/api/Account/Roles?email={1}&role={2}", WebApiUrl, email, role);
            UploadString(address, "POST", "");
        }

        public void RemoveRole(string email, string role)
        {
            var address = String.Format("{0}/api/Account/Roles?email={1}&role={2}", WebApiUrl, email, role);
            UploadString(address, "DELETE", "");
        }

        public void Logout()
        {
            Headers.Remove(HttpRequestHeader.Authorization);
            _token = null;
        }

        public OAuthToken GetAccessToken(string refreshToken)
        {
            try
            {
                var data = new NameValueCollection
                {
                    {"grant_type", "refresh_token"},
                    {"refresh_token", refreshToken},
                    {"client_id", "OwinRefreshTokenExample"}
                };
                //Headers.Remove(HttpRequestHeader.Authorization);
                Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var result = UploadValues(TokenEndpointUrl, data);
                LastOperationHttpStatusCode = HttpStatusCode.OK;
                LastOperationResponse = System.Text.Encoding.Default.GetString(result);

                var responseSerializer = new DataContractJsonSerializer(typeof(OAuthToken));

                using (var ms = new MemoryStream(result))
                {
                    _token = (OAuthToken)responseSerializer.ReadObject(ms);
                    Headers[HttpRequestHeader.Authorization] = "Bearer " + _token.access_token;
                }

                return _token;
            }
            catch (WebException e)
            {
                var response = (HttpWebResponse)e.Response;
                LastOperationHttpStatusCode = response.StatusCode;
                using (var s = response.GetResponseStream())
                using (var sr = new StreamReader(s))
                {
                    LastOperationResponse = sr.ReadToEnd();
                }
                return null;
            }
        }
    }

    [DataContract]
    public class OAuthToken
    {
        [DataMember(Name=".expires")] public string expires { get; set; }
        [DataMember(Name = ".issued")] public string issued { get; set; }
        [DataMember] public string access_token { get; set; }
        [DataMember] public string refresh_token { get; set; }
        [DataMember] public int expires_in { get; set; }
        [DataMember] public string token_type { get; set; }
        [DataMember(Name = "userName")] public string username { get; set; }
    }
}