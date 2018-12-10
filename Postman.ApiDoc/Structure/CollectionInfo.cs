using System;

namespace Vertical.Postman.ApiDoc.Structure
{
    /// <summary>
    /// Detailed description of the info block
    /// </summary>
    public class CollectionInfo
    {
        /// <summary>
        /// Every collection is identified by the unique value of this field. 
        /// The value of this field is usually easiest to generate using a UID generator function. 
        /// If you already have a collection, it is recommended that you maintain the same id since 
        /// changing the id usually implies that is a different collection than it was originally.
        /// </summary>
        /// <remarks>
        /// This field exists for compatibility reasons with Collection Format V1.
        /// </remarks>
        public Guid _Postman_Id { get; set; }

        /// <summary>
        /// A collection's friendly name is defined by this field. 
        /// You would want to set this field to a value that would allow you to easily identify this 
        /// collection among a bunch of other collections, as such outlining its usage or content.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// This should ideally hold a link to the Postman schema that is used to validate this collection. 
        /// </summary>
        public string Schema { get; set; }

        /// <summary>
        /// Postman allows you to version your collections as they grow, and this field holds the version number. 
        /// While optional, it is recommended that you use this field to its fullest extent!
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Description for collection
        /// </summary>
        public string Description { get; set; }
    }
}
