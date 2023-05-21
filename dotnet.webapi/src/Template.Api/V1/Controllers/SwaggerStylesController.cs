using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Template.Core.Item;

namespace Template.Api.V1.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[ApiController]
[Route("v{version:apiVersion}/[controller]")]
public class SwaggerStyles : ControllerBase
{

    [HttpGet(Name = "GetSwaggerStyles")]
    public ActionResult Get()
    {
        return Content(@"
            .swagger-ui .topbar{
              background:red;
            }
            ", "text/css");
    }


}

