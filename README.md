# netcore5-webapi
Demo .NET Core 5 (NET 5) Web API


# Demo 05: API Versioning
1. Install ```Microsoft.AspNetCore.Mvc.Versioning``` package
    ```
    dotnet add package Microsoft.AspNetCore.Mvc.Versioning --version 5.0.0
    ```
2. Go to ```StartUp.cs``` and update the ```ConfigureServices``` method with the following
    ```cs
    services.AddApiVersioning();
    ```
3. Add property before ApiController
    ```cs
    [ApiVersion("2.0")]
    public class BandsController : ControllerBase
    {
    }
    ```
4. You can add version for action
    ```cs
    [MapToApiVersion("2.0")]
    [HttpGet("{id}")]
    public async Task<ActionResult<Band>> GetById20(int id)
    {
    }
    ```
5. Demo 1: Query string-base versioning
    - Create controller ```TestController``` with 2 version and one action GET
    - Add property [ApiVersion("2.0")] before starting class TestController
    - Test call API:
        - /api/Test?api-version=1.0
        - /api/Test?api-version=2.0
6. Demo 2: Url based versioning
    - Create ```DemoController`` and setup route for this controller. Ex for version 1:
        ```cs
        [ApiVersion("1.0")]
        [Route("api/v{version:apiVersion}/[controller]")]
        [ApiController]
        public class DemoController : ControllerBase
        {
            [HttpGet]
            public IActionResult Get()
            {
                return Ok("Demo controller version 1.0");
            }
        }
        ```
    - Test API:
        * /api/v1/Demo
        * /api/v2/Demo
7. Demo 3: HTTP Header based versioning
    - Add custom:
        ```cs
        options.ApiVersionReader = new HeaderApiVersionReader("custom-version-header");
        ```
    - Testing: Using ```Postman``` to test. For specific API, in ```header``` tab:
        Add one row for ```Key | Value``` is:
        ```custom-version-header | 2.0```
8. Demo 4: HTTP Media-Type based versioning
    - Add custom:
        ```cs
        options.ApiVersionReader = new MediaTypeApiVersionReader("custom-version-header");
        ```
    - Testing: Using ```Postman``` to test. For specific API, in ```header``` tab: Add one row for ```Key | Value``` is:
        ```Content-Type | application/json;v=2.0```
        or:
        ```Accept | application/json;v=2.0```