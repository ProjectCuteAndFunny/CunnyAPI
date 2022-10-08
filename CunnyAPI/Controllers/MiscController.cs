using CunnyApi.v1.Globals;

using Microsoft.AspNetCore.Mvc;

namespace CunnyApi.v1.Controllers;

[Route("api")]
[ApiController]
public class MiscController : ControllerBase {
    [HttpGet]
    [Route("version")]
    public Version Get() {
        return BackendGlobals.ApiVersion;
    }
}
