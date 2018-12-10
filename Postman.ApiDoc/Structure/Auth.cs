namespace Vertical.Postman.ApiDoc.Structure
{
    /// <summary>
    /// Represents authentication helpers provided by Postman
    /// </summary>
    public class Auth
    {
        /// <summary>
        /// type of authentication method
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// bearer token
        /// </summary>
        public string Bearer { get; set; }
    }
}
