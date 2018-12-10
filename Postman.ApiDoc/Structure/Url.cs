using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vertical.Postman.ApiDoc.Structure
{
    public class Url
    {

        /// <summary>
        /// The string representation of the request URL, including the protocol, host, path, hash, query parameter(s) and path variable(s).
        /// </summary>
        public string Raw { get; set; }

        /// <summary>
        /// The protocol associated with the request, E.g: 'http'
        /// </summary>
        public string Protocol { get; set; }

        /// <summary>
        /// The host for the URL
        /// </summary>
        public string[] Host { get; set; }

        /// <summary>
        /// The port number present in this URL. An empty value implies 80/443 depending on whether the protocol field contains http/https
        /// </summary>
        public string Port { get; set; }

        public string[] Path { get; set; }

        /// <summary>
        /// An array of QueryParams, which is basically the query string part of the URL, parsed into separate variables
        /// </summary>
        public QueryParam[] Query { get; set; }

        /// <summary>
        /// Contains the URL fragment (if any). Usually this is not transmitted over the network, but it could be useful to store this in some cases
        /// </summary>
        public string Hash { get; set; }
    }
}
