namespace Postman.ApiDoc.Tests.Environment {
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;

    namespace PackEx.Api.Tests.Helpers
    {

        /// <summary>
        /// extensions for testserver
        /// </summary>
        public static class TestServerExtensions
        {

            /// <summary>
            /// get a service from test server
            /// </summary>
            /// <typeparam name="T">type of service to get</typeparam>
            /// <param name="server">server of which to get service</param>
            /// <returns></returns>
            public static T GetService<T>(this TestServer server)
            {
                return server.Host.Services.GetService<T>();
            }

        }
    }
}