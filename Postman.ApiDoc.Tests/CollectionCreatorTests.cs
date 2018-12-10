using System;
using System.Linq;
using System.Security.Policy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Postman.ApiDoc.Tests.Environment;
using Postman.ApiDoc.Tests.Environment.PackEx.Api.Tests.Helpers;
using Vertical.Postman.ApiDoc;
using Vertical.Postman.ApiDoc.Authentication;
using Vertical.Postman.ApiDoc.Structure;

namespace Postman.ApiDoc.Tests
{
    [TestClass]
    public class CollectionCreatorTests
    {
        [TestMethod]
        public void CreateCollection()
        {
            IWebHostBuilder webhostbuilder = new WebHostBuilder()
                .UseEnvironment("Test")
                .UseStartup<TestStartup>();
            
            using (TestServer server = new TestServer(webhostbuilder)) {
                PostmanCollectionCreator collectioncreator = new PostmanCollectionCreator("http://localhost:5000");
                Collection collection=collectioncreator.Create(Guid.Empty, "Test", server.GetService<IApiDescriptionGroupCollectionProvider>());
                Assert.AreNotEqual(0, collection.Item.Length);
            }
        }

        [TestMethod]
        public void QueryParametersAreAdded() {
            IWebHostBuilder webhostbuilder = new WebHostBuilder()
                .UseEnvironment("Test")
                .UseStartup<TestStartup>();

            using (TestServer server = new TestServer(webhostbuilder))
            {
                PostmanCollectionCreator collectioncreator = new PostmanCollectionCreator("http://localhost:5000");
                Collection collection = collectioncreator.Create(Guid.Empty, "Test", server.GetService<IApiDescriptionGroupCollectionProvider>());
                Assert.AreNotEqual(0, collection.Item.Length);

                RequestItem item = collection["Common.Simple.GetSomethingWithQuery"] as RequestItem;
                Assert.IsNotNull(item);
                Assert.AreEqual(1, item.Request.Url.Query.Length);
                Assert.AreEqual("parameter", item.Request.Url.Query[0].Key);
            }
        }

        [TestMethod]
        public void UserPasswordAuthentication() {
            IWebHostBuilder webhostbuilder = new WebHostBuilder()
                .UseEnvironment("Test")
                .UseStartup<TestStartup>();

            using (TestServer server = new TestServer(webhostbuilder)) {
                PostmanCollectionCreator collectioncreator = new PostmanCollectionCreator("http://localhost:5000", new AuthenticationInfo {
                    Path = new Uri("http://auth.localhost:5000/"),
                    AuthenticationData = new AuthenticationData {
                        Audience = "audience",
                        ClientId = "clientid",
                        GrantType = "test",
                        Username = "user",
                        Password = "password",
                        Realm = "realm",
                        Scope = "scopes"
                    }
                });
                Collection collection = collectioncreator.Create(Guid.Empty, "Test", server.GetService<IApiDescriptionGroupCollectionProvider>());
                Assert.AreNotEqual(0, collection.Item.Length);

                Assert.IsTrue(collection.Item.Any(i => i.Name.StartsWith("Authenticate")));
            }
        }
    }
}
