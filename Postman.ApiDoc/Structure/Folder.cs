using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vertical.Postman.ApiDoc.Structure
{

    /// <summary>
    /// folder in a postman collection
    /// </summary>
    public class Folder : Item
    {
        /// <summary>
        /// items in folder
        /// </summary>
        public Item[] Item { get; set; }
    }
}
