# netcore5-webapi
Demo .NET Core 5 (NET 5) Web API


# Demo 03: Controller Action Return Type

1. API GET
    - Success: HTTP Status 200
        ```cs
        return Ok(_response);
        ```
    - Fail: HTTP Status 404
        ```cs
        return NotFound();
        ```
2. API POST
    - Success: HTTP Status Code 201
        ```cs
        return Created(nameof(AddBook), newPublisher);
        ```
3. try ... catch: Return ```BadRequest``` for exception
    ```cs
     return BadRequest(ex.Message);
     ```
