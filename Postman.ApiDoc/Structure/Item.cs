using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vertical.Postman.ApiDoc.Structure
{

    /// <summary>
    /// item in a postman folder
    /// </summary>
    public class Item
    {
        /// <summary>
        /// name of node
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// variables valid in this item scope
        /// </summary>
        public Variable[] Variable { get; set; }
    }
}
