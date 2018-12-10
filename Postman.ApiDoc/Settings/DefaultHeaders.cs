using System;
using System.Collections.Generic;
using System.Text;
using Vertical.Postman.ApiDoc.Structure;

namespace Vertical.Postman.ApiDoc.Settings
{
    /// <summary>
    /// default values for headers used in <see cref="Collection"/>s
    /// </summary>
    public static class DefaultHeaders
    {
        /// <summary>
        /// content type to use for json data
        /// </summary>
        public static readonly Parameter ContentTypeJson = new Parameter
        {
            Key = "Content-Type",
            Value = "application/json",
            Description = "Data type of body"
        };

        /// <summary>
        /// content type to use for binary data
        /// </summary>
        public static readonly Parameter ContentTypeBinary = new Parameter
        {
            Key = "Content-Type",
            Value = "application/octet-stream",
            Description = "Data type of body"
        };
    }
}
