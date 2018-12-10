using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vertical.Postman.ApiDoc.Structure
{
    /// <summary>
    /// request information
    /// </summary>
    public class RequestItem : Item
    {
        /// <summary>
        /// events for request
        /// </summary>
        public Event[] Event { get; set; }

        public Request Request { get; set; }

        public Response Response { get; set; }
    }
}
