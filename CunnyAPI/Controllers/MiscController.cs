using CunnyApi.v1.Globals;

using Microsoft.AspNetCore.Mvc;

namespace CunnyApi.v1.Controllers;

[Route("api")]
[ApiController]
public class MiscController : ControllerBase {
    [HttpGet]
    [Route("version")]
    public Version GetVersion() {
        return BackendGlobals.ApiVersion;
    }
    [HttpGet]
    [Route("latest")]
    public string GetLatestRoute() {
        return BackendGlobals.LatestVersionRoute;
    }
}
