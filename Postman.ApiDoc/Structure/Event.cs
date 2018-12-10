using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vertical.Postman.ApiDoc.Structure
{

    /// <summary>
    /// script for an event
    /// </summary>
    public class Event
    {

        /// <summary>
        /// name of event to listen to
        /// </summary>
        public string Listen { get; set; }

        /// <summary>
        /// script for event
        /// </summary>
        public Script Script { get; set; }
    }
}
