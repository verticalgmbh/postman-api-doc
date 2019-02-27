using Newtonsoft.Json;

namespace Vertical.Postman.ApiDoc.Authentication {

    /// <summary>
    /// interface for authentication data to include
    /// </summary>
    public interface IAuthenticationData {

        /// <summary>
        /// name of request
        /// </summary>
        [JsonIgnore]
        string Name { get;  }

        /// <summary>
        /// request description
        /// </summary>
        [JsonIgnore]
        string Description { get; }

        /// <summary>
        /// method to use for request
        /// </summary>
        [JsonIgnore]
        string Method { get; }

        /// <summary>
        /// server to which to send request
        /// </summary>
        string Server { get; set; }
    }
}