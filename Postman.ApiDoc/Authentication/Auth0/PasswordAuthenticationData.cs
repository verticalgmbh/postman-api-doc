namespace Vertical.Postman.ApiDoc.Authentication.Auth0
{

    /// <summary>
    /// data used for authentication
    /// </summary>
    public class PasswordAuthenticationData : IAuthenticationData
    {
        /// <summary>
        /// client id of application
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// grant type to use for authentication
        /// </summary>
        public string GrantType => "http://auth0.com/oauth/grant-type/password-realm";

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
        public string Realm => "Username-Password-Authentication";

        /// <summary>
        /// audience for which to get authentication token
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// authentication scope
        /// </summary>
        public string Scope { get; set; } = "openid";

        /// <inheritdoc />
        public string Name => "Authenticate (Get User Token)";

        /// <inheritdoc />
        public string Description => "Authenticates a user and provides the authentication token for all requests";

        /// <inheritdoc />
        public string Method => "POST";

        /// <inheritdoc />
        public string Server { get; set; }
    }
}
