using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using Vertical.Postman.ApiDoc.Authentication;
using Vertical.Postman.ApiDoc.Json;
using Vertical.Postman.ApiDoc.Settings;
using Vertical.Postman.ApiDoc.Structure;

namespace Vertical.Postman.ApiDoc
{

    /// <summary>
    /// creates a postman collection from api data
    /// </summary>
    public class PostmanCollectionCreator
    {
        readonly string host;
        readonly IAuthenticationData[] authentication;

        /// <summary>
        /// creates a new <see cref="PostmanCollectionCreator"/>
        /// </summary>
        /// <param name="host">host where server is running on</param>
        /// <param name="authentication">authentication methods to include</param>
        public PostmanCollectionCreator(string host, params IAuthenticationData[] authentication)
        {
            this.host = host;
            this.authentication = authentication;
        }

        /// <summary>
        /// settings for creation of collection
        /// </summary>
        public CollectionCreatorSettings Settings { get; } = CollectionCreatorSettings.Default;

        IEnumerable<Variable> GetCollectionVariables()
        {
            yield return Variable.Create("hostname", host, "Host where api is running on");
            yield return Variable.Create("auth_token", null, "Bearer token used for authorization");
            if (authentication != null) {
                yield return Variable.Create("client_id", "", "Client ID of authenticated APIs");
                yield return Variable.Create("client_secret", "", "Client Secret of authenticated APIs");
            }
        }

        /// <summary>
        /// creates a <see cref="Collection"/> using a <see cref="IApiDescriptionGroupCollectionProvider"/>
        /// </summary>
        /// <param name="documentid">id of document for identification in postman (only used for compatibility with old postman versions)</param>
        /// <param name="name">name of the collection to create</param>
        /// <param name="apidescription"></param>
        /// <returns>collection containing api requests usable for tests in postman</returns>
        public Collection Create(Guid documentid, string name, IApiDescriptionGroupCollectionProvider apidescription)
        {
            XmlDocument documentation = new XmlDocument();
            string xmldocpath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, $"{Assembly.GetEntryAssembly().GetName().Name}.xml");
            if (File.Exists(xmldocpath))
                documentation.Load(xmldocpath);

            return new Collection {
                Info = new CollectionInfo
                {
                    _Postman_Id = documentid,
                    Name = name,
                    Schema = "https://schema.getpostman.com/json/collection/v2.1.0/collection.json"
                },
                Item = CreateItems(apidescription.ApiDescriptionGroups.Items, documentation).ToArray(),
                Variable = GetCollectionVariables().ToArray()
            };
        }

        Url ExtractUrl(ApiDescription description)
        {
            string[] elements = description.RelativePath.Split('/');
            elements = elements.Select(e =>
            {
                if (!e.StartsWith("{"))
                    return e;

                Match match = Regex.Match(e, "^{(?<name>[a-zA-Z0-9_]+)(:(?<type>[a-zA-Z0-9_]+))?}$");
                if (match.Success)
                {
                    return "{{" + match.Groups["name"].Value + "}}";
                }

                return e;
            }).ToArray();

            return new Url {
                Raw = $"{{{{hostname}}}}{string.Join("/", elements)}",
                Host = new[] {"{{hostname}}"},
                Path = elements,
                Query = description.ParameterDescriptions.Where(p => p.Source.Id == "Query").Select(p => new QueryParam {
                    Key = p.Name
                }).ToArray()
            };
        }

        RawBody ExtractBody(ApiDescription description)
        {
            ApiParameterDescription body = description.ParameterDescriptions.FirstOrDefault(d => d.Source.Id == "Body");
            if (body == null)
                return null;

            object example;
            if (body.ParameterDescriptor.ParameterType == typeof(IFormFile))
                example = "Binary Data";
            else {
                try {
                    example = Settings.ExampleProvider((description.ActionDescriptor as ControllerActionDescriptor)?.MethodInfo, body.ParameterDescriptor.ParameterType);
                }
                catch (Exception) {
                    example = "Unable to create example data";
                }
                
            }


            return new RawBody
            {
                Mode = "raw",
                Raw = JsonConvert.SerializeObject(example, Newtonsoft.Json.Formatting.Indented, JsonSettings.Body)
            };
        }

        IEnumerable<Variable> ExtractVariables(ApiDescription description)
        {
            foreach (ApiParameterDescription parameter in description.ParameterDescriptions.Where(p => p.Source.Id == "Path"))
            {
                yield return new Variable
                {
                    Id = Guid.NewGuid(),
                    Key = parameter.Name
                };
            }
        }

        Item CreateItem(ApiDescription description, XmlDocument documentation)
        {
            return new RequestItem
            {
                Name = (description.ActionDescriptor as ControllerActionDescriptor)?.MethodInfo?.Name,
                Request = new Request
                {
                    Method = description.HttpMethod,
                    Description = ExtractSummary((description.ActionDescriptor as ControllerActionDescriptor)?.MethodInfo, documentation),
                    Url = ExtractUrl(description),
                    Body = ExtractBody(description),
                    //Auth=ExtractAuth(description),
                    Header = ExtractAuth(description).ToArray()
                },
                Variable = ExtractVariables(description).ToArray()
            };
        }

        string ExtractSummary(MethodInfo methodInfo, XmlDocument documentation) {
            if (methodInfo.DeclaringType == null)
                return "";

            string nodepath = $"M:{methodInfo.DeclaringType.FullName}.{methodInfo.Name}";
            XmlNode memberdoc = documentation.SelectSingleNode($"//member[starts-with(@name, '{nodepath}')]/summary");
            if (memberdoc == null)
                return "";
            return memberdoc.InnerText.Trim();
        }

        IEnumerable<Parameter> ExtractAuth(ApiDescription description) {
            if (!(description.ActionDescriptor is ControllerActionDescriptor))
                yield break;

            if (Attribute.IsDefined(((ControllerActionDescriptor) description.ActionDescriptor)?.MethodInfo, typeof(AuthorizeAttribute)))
            {
                yield return new Parameter
                {
                    Key = "Authorization",
                    Value = "Bearer {{auth_token}}",
                    Description = "Token used for authentication"
                };
            }

            ApiParameterDescription body = description.ParameterDescriptions.FirstOrDefault(d => d.Source.Id == "Body");
            if (body != null)
            {
                if (body.ParameterDescriptor.ParameterType == typeof(IFormFile))
                    yield return DefaultHeaders.ContentTypeBinary;
                else
                    yield return DefaultHeaders.ContentTypeJson;
            }
        }

        IEnumerable<Item> CreateGroupItem(ApiDescriptionGroup group, XmlDocument documentation)
        {
            foreach (IGrouping<string, ApiDescription> controller in group.Items.GroupBy(Settings.GroupExtractor))
            {
                yield return new Folder
                {
                    Name = controller.Key,
                    Item = controller.Select(i => CreateItem(i, documentation)).ToArray()
                };
            }
        }

        Url Convert(Uri uri)
        {
            return new Url
            {
                Raw = uri.ToString(),
                Protocol = uri.Scheme,
                Host = new[]
                {
                    uri.Host+(uri.IsDefaultPort?"":$":{uri.Port}")
                },
                Path = uri.AbsolutePath.Split('/')
            };
        }

        RequestItem CreateAuthenticationPassword(IAuthenticationData authdata)
        {
            return new RequestItem
            {
                Name = authdata.Name,
                Request = new Request
                {
                    Description = authdata.Description,
                    Method = authdata.Method,
                    Url = Convert(new Uri(authdata.Server)),
                    Body = new RawBody
                    {
                        Mode = "raw",
                        Raw = JsonConvert.SerializeObject(authdata, Newtonsoft.Json.Formatting.Indented, JsonSettings.Authentication
                        )
                    },
                    Header=new[] {
                        DefaultHeaders.ContentTypeJson
                    }
                },
                Event = new[]{
                        new Event
                        {
                            Listen="test",
                            Script=new Script
                            {
                                Id=Guid.NewGuid(),
                                Type="text/javascript",
                                Exec = new[]
                                {
                                    "var jsonData = JSON.parse(responseBody);",
                                    "postman.setEnvironmentVariable(\"auth_token\", jsonData.access_token);",
                                }
                            }
                        }
                    }
            };
        }

        private IEnumerable<Item> CreateItems(IEnumerable<ApiDescriptionGroup> apidescriptiongroups, XmlDocument documentation)
        {
            if (authentication != null) {
                foreach (IAuthenticationData authdata in authentication)
                    yield return CreateAuthenticationPassword(authdata);
            }

            foreach (ApiDescriptionGroup group in apidescriptiongroups)
            {
                string groupname = string.IsNullOrEmpty(group.GroupName) ? "Common" : group.GroupName;
                yield return new Folder
                {
                    Name = groupname,
                    Item = CreateGroupItem(group, documentation).ToArray()
                };
            }
        }
    }
}
