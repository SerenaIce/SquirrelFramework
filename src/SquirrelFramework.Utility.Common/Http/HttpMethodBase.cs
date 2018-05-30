namespace SquirrelFramework.Utility.Common.Http
{
    using System.IO;
    using System.Net;

    public abstract class HttpMethodBase
    {
        protected HttpMethodBase()
        {
            this.ContentType = "application/json";
        }

        protected string ServerBaseUrl { get; set; }
        protected CookieContainer Cookies { get; set; }
        protected string ContentType { get; set; }

        protected HttpWebRequest RequestInternal(string url, string httpMethod)
        {
            var request = WebRequest.CreateHttp($"{this.ServerBaseUrl}/{url}");
            request.Method = httpMethod;
            request.ContentType = this.ContentType;
            request.CookieContainer = this.Cookies;
            return request;
        }

        protected HttpWebRequest Get(string url)
        {
            return this.RequestInternal(url, "GET");
        }

        protected HttpWebRequest Get(string url, QueryString queryString)
        {
            return this.RequestInternal(url + queryString.ToString(), "GET");
        }


        protected HttpWebRequest Post(string url)
        {
            return this.RequestInternal(url, "POST");
        }
        protected HttpWebRequest Post(string url, QueryString queryString)
        {
            return this.RequestInternal(url + queryString.ToString(), "POST");
        }

        protected HttpWebRequest Put(string url)
        {
            return this.RequestInternal(url, "PUT");
        }

        protected HttpWebRequest Delete(string url)
        {
            return this.RequestInternal(url, "DELETE");
        }

        protected string GetResponse(HttpWebRequest request)
        {
            using (var response = request.GetResponse() as HttpWebResponse)
            {
                var reader = new StreamReader(response.GetResponseStream());
                return reader.ReadToEnd();
            }
        }

        protected T GetResponse<T>(HttpWebRequest request)
        {
            return JsonHelper.Deserialize<T>(this.GetResponse(request));
        }

        protected void PostData(HttpWebRequest request, JsonString postData)
        {
            this.PostData(request, postData.ToString());
        }

        protected void PostData(HttpWebRequest request, string postData)
        {
            using (var postStream = new StreamWriter(request.GetRequestStream()))
            {
                postStream.Write(postData);
            }
        }
    }
}