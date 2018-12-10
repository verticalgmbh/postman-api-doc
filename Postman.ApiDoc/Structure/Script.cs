using System;

namespace Vertical.Postman.ApiDoc.Structure
{

    /// <summary>
    /// script for <see cref="Event"/>
    /// </summary>
    public class Script
    {

        /// <summary>
        /// script id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// type of script
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// script code
        /// </summary>
        public string[] Exec { get; set; }
    }
}
