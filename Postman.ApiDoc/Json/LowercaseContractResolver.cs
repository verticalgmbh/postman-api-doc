using Newtonsoft.Json.Serialization;

namespace Vertical.Postman.ApiDoc.Json
{

    /// <summary>
    /// contract resolver which returns property in all lower case
    /// </summary>
    public class LowercaseContractResolver : DefaultContractResolver
    {
        /// <inheritdoc/>
        protected override string ResolvePropertyName(string propertyname)
        {
            return propertyname.ToLower();
        }
    }
}
