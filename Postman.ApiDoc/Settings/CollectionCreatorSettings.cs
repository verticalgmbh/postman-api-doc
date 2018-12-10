using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Reflection;

namespace Vertical.Postman.ApiDoc.Settings
{

    /// <summary>
    /// settings for <see cref="PostmanCollectionCreator"/>
    /// </summary>
    public class CollectionCreatorSettings
    {

        /// <summary>
        /// method used to extract group information
        /// </summary>
        public Func<ApiDescription, string> GroupExtractor { get; set; }

        /// <summary>
        /// method used to get examples for request body
        /// </summary>
        public Func<MethodInfo,Type,object> ExampleProvider { get; set; }

        /// <summary>
        /// default settings used by <see cref="PostmanCollectionCreator"/>
        /// </summary>
        internal static CollectionCreatorSettings Default => new CollectionCreatorSettings
        {
            GroupExtractor = DefaultMethods.GetGroup,
            ExampleProvider = DefaultMethods.GetExample
        };
    }
}
