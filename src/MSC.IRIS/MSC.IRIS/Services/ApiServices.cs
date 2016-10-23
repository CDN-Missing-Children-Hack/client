using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MSC.IRIS.Models
{
    // TODO : need to harden all calls with try catch

    /// <summary>
    /// Access to the backend REST API service
    /// </summary>
    public class ApiServices
    {
        private const string BASE_URL = "http://mcsc-clientapi-dev.azurewebsites.net/api/{0}/";
        private const string LOGIN_URL = "login";
        private const string GET_CASES_URL = "cases";
        private const string GET_CASE_URL = "cases/{0}";
        private const string WATCH_CASE_URL = "cases/{0}/watch";

        private string _authToken = null;

        public async Task<PoliceUser> Login (string username, string password)
        {
            // compose the data
            var kvps = new [] {
                new KeyValuePair<string,string>("username", username),
                new KeyValuePair<string,string>("password", password),
            };

            // encode the content
            var content = new FormUrlEncodedContent (kvps);

            // create the message
            var url = string.Format (BASE_URL, LOGIN_URL);
            var request = new HttpRequestMessage (HttpMethod.Post, url);
            request.Content = content;

            try
            {
                // send the request
                var ret = await SendRequest (request);

                if (ret.StatusCode == HttpStatusCode.OK)
                {
                    // deserilaize the token
                    var d = JsonConvert.DeserializeObject<PoliceUser> (ret.Content);

                    // save the auth token
                    this._authToken = d.Token;

                    // return the user object
                    return d;
                }
            }
            catch 
            {
                // there was an exception so just ignore for now but needs to be properly handled
                //return new PoliceUser { Username = username, FirstName = "TODO", LastName = "TODO", Token = "TODO" };
            }

            // if we get here just return null
            return null;
        }

        public async Task<List<Case>> GetCases (PoliceUser user)
        {
            // create the request
            var url = string.Format (BASE_URL, GET_CASES_URL);
            var request = new HttpRequestMessage (HttpMethod.Get, url);

            // the object to return
            var ret = new List<Case> ();
            try
            {
                // send the request
                var resp = await SendRequest (request);

                // parse the data if status code is OK
                if (resp.StatusCode == HttpStatusCode.OK)
                    ret = JsonConvert.DeserializeObject<List<Case>> (resp.Content);

            }
            catch
            {
                // there was an exception so just ignore for now but needs to be properly handled
            }

            // return the data
            return ret;
        }

        public async Task<Case> GetCase (int caseId)
        {
            // create the url
            var url = string.Format (string.Format (BASE_URL, GET_CASE_URL), caseId);
            var request = new HttpRequestMessage (HttpMethod.Get, url);

            // the object to return
            var ret = default (Case);
            try
            {
                // send the request
                var resp = await SendRequest (request);

                // parse the data if status code is OK
                if (resp.StatusCode == HttpStatusCode.OK)
                    ret = JsonConvert.DeserializeObject<Case> (resp.Content);

            }
            catch
            {
                // there was an exception so just ignore for now but needs to be properly handled
            }

            // return the data
            return ret;
        }

        private async Task<HttpServerResponse> SendRequest (HttpRequestMessage request)
        {
            // add the auth header 
            if (string.IsNullOrEmpty (_authToken))
                request.Headers.Add ("AUTHORIZATION", $"bearer {_authToken}");

            var client = new HttpClient ();

            // send the data
            var response = await client.SendAsync (request);

            // check for response and return it
            var content = await this.GetRequestContent (response);
            return new HttpServerResponse
            {
                Content = content,
                StatusCode = response.StatusCode
            };
        }

        private async Task<string> GetRequestContent (HttpResponseMessage response)
        {
            var content = string.Empty;
            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                using (response.Content)
                {
                    content = await response.Content.ReadAsStringAsync ();
                }
            }
            return content;
        }

        public class HttpServerResponse
        {
            /// <summary>
            /// Gets or sets the status code return by the server
            /// </summary>
            /// <value>The status code.</value>
            public HttpStatusCode StatusCode
            {
                get;
                set;
            }

            /// <summary>
            /// Gets or sets the content returned by the server
            /// </summary>
            /// <value>The content.</value>
            public string Content
            {
                get;
                set;
            }
        }
    }
}
