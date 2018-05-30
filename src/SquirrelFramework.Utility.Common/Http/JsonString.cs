namespace SquirrelFramework.Utility.Common.Http
{
    #region using directives

    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    #endregion using directives

    public class JsonString : Dictionary<string, string>
    {
        public string Generate()
        {
            var result = new StringBuilder();
            // It must " not '
            result.Append("{");
            foreach (var pair in this)
            {
                result.Append("\"" + pair.Key + "\": ");
                result.Append("\"" + pair.Value + (pair.Equals(this.Last()) ? "\"" : "\", "));
            }
            result.Append("}");
            return result.ToString();
        }

        public override string ToString()
        {
            return this.Generate();
        }
    }
}