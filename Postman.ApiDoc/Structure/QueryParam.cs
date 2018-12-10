namespace Vertical.Postman.ApiDoc.Structure {

    /// <summary>
    /// query parameter of a <see cref="Request"/>
    /// </summary>
    public class QueryParam {

        /// <summary>
        /// key of query parameter
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// value of query parameter
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// description for parameter
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// If set to true, the current query parameter will not be sent with the request
        /// </summary>
        public bool Disabled { get; set; }
        
    }
}