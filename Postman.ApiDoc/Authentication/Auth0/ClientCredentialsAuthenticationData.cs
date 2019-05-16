namespace Vertical.Postman.ApiDoc.Authentication.Auth0 {

    /// <summary>
    /// authentication data used for machine 2 machine authentication
    /// </summary>
    public class ClientCredentialsAuthenticationData : IAuthenticationData {

        /// <summary>
        /// audience for which to get authentication token
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// authentication type to use
        /// </summary>
        public string GrantType => "client_credentials";

        /// <summary>
        /// client id of application
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// client secret of api to authenticate
        /// </summary>
        public string ClientSecret { get; set; }

        /// <inheritdoc />
        public string Name => "Authenticate (Get Machine Token)";

        /// <inheritdoc />
        public string Description => "Authenticates an application and provides the authentication token for all requests";

        /// <inheritdoc />
        public string Method => "POST";

        /// <inheritdoc />
        public string Server { get; set; }

        /// <inheritdoc />
        public string Scope { get; set; }
    }
}