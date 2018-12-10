namespace Vertical.Postman.ApiDoc.Authentication
{

    /// <summary>
    /// data used for authentication
    /// </summary>
    public class AuthenticationData
    {
        /// <summary>
        /// client id of application
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// grant type to use for authentication
        /// </summary>
        public string GrantType { get; set; }

        /// <summary>
        /// username used to login
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// password used to login
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// realm name
        /// </summary>
        public string Realm { get; set; }

        /// <summary>
        /// audience for which to get authentication token
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// authentication scope
        /// </summary>
        public string Scope { get; set; }
    }
}
