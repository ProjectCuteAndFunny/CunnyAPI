using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CunnyApi.v1.Controllers;

[ApiController]
public class TestController : ControllerBase {
    [HttpGet]
    [Route("test")]
    public async Task<string[]> Get([FromHeader] string testHeader) {
        return new string[] {
            "UOOH CUNNY",
            testHeader
        };
    }
}
