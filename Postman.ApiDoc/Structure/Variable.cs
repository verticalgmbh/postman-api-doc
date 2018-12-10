using System;

namespace Vertical.Postman.ApiDoc.Structure
{

    /// <summary>
    /// variable in postman
    /// </summary>
    public class Variable
    {
        /// <summary>
        /// id of variable
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// name of variable
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// value type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// variable description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// creates a new <see cref="Variable"/>
        /// </summary>
        /// <param name="name">name of variable</param>
        /// <param name="value">initial variable value (optional)</param>
        /// <param name="description">variable description (optional)</param>
        /// <returns>postman variable</returns>
        internal static Variable Create(string name, string value=null, string description = null)
        {
            return new Variable
            {
                Id = Guid.NewGuid(),
                Key = name,
                Description = description,
                Value = value,
                Type = "string"
            };
        }
    }
}
