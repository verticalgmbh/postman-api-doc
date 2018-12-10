namespace Vertical.Postman.ApiDoc.Structure
{

    /// <summary>
    /// parameter in postman
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// key for header item
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// value for header item
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// description for parameter
        /// </summary>
        public string Description { get; set; }
    }
}
