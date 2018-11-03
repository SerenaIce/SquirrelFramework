namespace SquirrelFramework.Utility.Common.Http
{
    #region using directives

    using System;
    using Newtonsoft.Json;

    #endregion

    public class JsonHelper
    {
        //private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
        //{
        //    ContractResolver = new CamelCasePropertyNamesContractResolver()
        //};

        public static dynamic Deserialize(string jsonString)
        {
            return JsonConvert.DeserializeObject(jsonString);
        }

        public static T Deserialize<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static object Deserialize(string jsonString, Type type)
        {
            return JsonConvert.DeserializeObject(jsonString, type);
        }

        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}