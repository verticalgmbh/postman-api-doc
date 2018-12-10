using System;

namespace Vertical.Postman.ApiDoc.Authentication
{

    /// <summary>
    /// info how to get authentication info
    /// </summary>
    public class AuthenticationInfo
    {
        /// <summary>
        /// path to call for authentication
        /// </summary>
        public Uri Path { get; set; }

        /// <summary>
        /// data to send to authentication server
        /// </summary>
        public AuthenticationData AuthenticationData { get; set; }

    }
}
