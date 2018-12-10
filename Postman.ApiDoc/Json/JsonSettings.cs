using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Vertical.Postman.ApiDoc.Json
{
    /// <summary>
    /// settings used when serializing json data
    /// </summary>
    public static class JsonSettings
    {

        /// <summary>
        /// serializer settings for body data
        /// </summary>
        public static JsonSerializerSettings Body = new JsonSerializerSettings
        {
            ContractResolver = new LowercaseContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
        };

        /// <summary>
        /// serializer settings for authentication body
        /// </summary>
        public static JsonSerializerSettings Authentication = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            },
            NullValueHandling = NullValueHandling.Ignore
        };
    }
}
