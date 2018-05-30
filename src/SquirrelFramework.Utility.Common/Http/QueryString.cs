namespace SquirrelFramework.Utility.Common.Http
{
    #region using directives

    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;

    #endregion using directives


    public class QueryString : Dictionary<string, string>
    {
        public NameValueCollection ParseQueryStringAsCollection(string queryString)
        {
            return System.Web.HttpUtility.ParseQueryString(queryString);
        }

        public QueryString ParseQueryString(string queryString)
        {
            var collection = System.Web.HttpUtility.ParseQueryString(queryString);
            return collection.AllKeys.ToDictionary(k => k, k => collection[k]) as QueryString;
        }

        public string Generate()
        {
            var removeList = (from pair in this where string.IsNullOrEmpty(pair.Value) select pair.Key).ToList();
            foreach (var item in removeList)
            {
                this.Remove(item);
            }

            var result = new StringBuilder();
            result.Append("?");
            foreach (var pair in this)
            {
                result.Append(pair.Key + "=");
                result.Append(pair.Value + (pair.Equals(this.Last()) ? "" : "&"));
            }
            return result.ToString();
        }

        public override string ToString()
        {
            return this.Generate();
        }
    }
}