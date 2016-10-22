using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MSC.IRIS.Models
{
    // TODO : need to harden all calls with try catch

    /// <summary>
    /// Access to the backend REST API service
    /// </summary>
    public class ApiServices
    {
        private const string BASE_URL = "https://what-is-this-url.com/api/{0}";
        private const string LOGIN_URL = "login";
        private const string GET_CASES_URL = "cases";
        private const string GET_CASE_URL = "cases/{0}";
        private const string WATCH_CASE_URL = "cases/{0}/watch";

        public async Task<PoliceUser> Login (string username, string password)
        {
            // TODO : this will have to be refactored depending on the type of auth we do ie OAuth

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
                await SendRequest (request);

                // compose a police user to return
                return new PoliceUser { Username = username, FirstName = "TODO", LastName = "TODO", Token = "TODO" };
            }
            catch 
            {
                // there was an exception so just ignore for now but needs to be properly handled
                return null;
            }
        }

        private async Task<HttpServerResponse> SendRequest (HttpRequestMessage request)
        {
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
