namespace Vertical.Postman.ApiDoc.Structure
{
    public class Request
    {

        /// <summary>
        /// request method
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// description for method
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// header for request
        /// </summary>
        public Parameter[] Header { get; set; }

        public RawBody Body { get; set; }

        public Url Url { get; set; }

        /// <summary>
        /// authentication for request
        /// </summary>
        public Auth Auth { get; set; }
    }
}
