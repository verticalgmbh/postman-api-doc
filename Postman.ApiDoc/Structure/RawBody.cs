using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vertical.Postman.ApiDoc.Structure
{
    public class RawBody
    {
        /// <summary>
        /// type of body
        /// </summary>
        public string Mode { get; set; }

        /// <summary>
        /// content of body
        /// </summary>
        public string Raw { get; set; }
    }
}
