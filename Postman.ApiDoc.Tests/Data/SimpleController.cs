using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Postman.ApiDoc.Tests.Data {

    [ApiController]
    [Route("api/[controller]")]
    public class SimpleController : Controller {

        [HttpGet]
        public string Get() {
            return "Works";
        }

        [HttpGet("query")]
        public string GetSomethingWithQuery([FromQuery]string parameter) {
            return "Works";
        }

        [HttpPost("post")]
        public void PostSomeRequest([FromBody] SimpleRequest request) {
        }

        [HttpPost("file")]
        public void Upload([FromBody]IFormFile formfile) { }

        [HttpGet("authorization")]
        [Authorize]
        public void AuthorizedPath() { }

        [HttpGet("query/{param}")]
        public void QueryFromPath(string param) { }
    }
}