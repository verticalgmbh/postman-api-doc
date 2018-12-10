using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Reflection;

namespace Vertical.Postman.ApiDoc.Settings
{

    /// <summary>
    /// default methods used in <see cref="CollectionCreatorSettings"/>
    /// </summary>
    public static class DefaultMethods
    {

        /// <summary>
        /// default method used to get an example for a datatype
        /// which creates the type and fills it with empty values
        /// </summary>
        /// <param name="method">method for which to get example data</param>
        /// <param name="requesttype">type of request data</param>
        /// <returns>example object</returns>
        public static object GetExample(MethodInfo method, Type requesttype)
        {
            object example = null;
            try
            {
                example = Activator.CreateInstance(requesttype);
            }
            catch(Exception)
            {
                return $"Unable to create example data instance of '{requesttype}'";
            }

            foreach (PropertyInfo property in requesttype.GetProperties())
                if (property.CanWrite)
                {
                    if (property.PropertyType == typeof(string))
                        property.SetValue(example, "");
                    else if (property.PropertyType.IsArray)
                        property.SetValue(example, Array.CreateInstance(property.PropertyType.GetElementType(), 0));
                    else property.SetValue(example, Activator.CreateInstance(property.PropertyType));
                }
            return example;
        }

        /// <summary>
        /// get group name for an api endpoint
        /// </summary>
        /// <param name="api">api for which to extract group</param>
        /// <returns>group name of api endpoint</returns>
        public static string GetGroup(ApiDescription api)
        {
            ControllerActionDescriptor controllerdescriptor = api.ActionDescriptor as ControllerActionDescriptor;
            if (controllerdescriptor == null)
                return api.GroupName;
            return controllerdescriptor.ControllerName;
        }
    }
}
