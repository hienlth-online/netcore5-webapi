# netcore5-webapi
Demo .NET Core 5 (NET 5) Web API


# Demo 06: Web API Logging with Serilog
1. Install Serilog package
    - ```Serilog.AspNetCore```
    - ```Serilog.Sinks.File``` for store log in file
    - ```Serilog.Sinks.MSSqlServer``` for store log in database
2. Setup logging
    - In ```Main``` method, edit with content below:
        ```cs
        try
        {
            Log.Logger = new LoggerConfiguration().CreateLogger();
            CreateHostBuilder(args).Build().Run();
        }
        catch
        {
            Log.CloseAndFlush();
        }
        ```
3. Store log in text file
    - Step 1: Config save in one file
        Log.Logger = new LoggerConfiguration()
            ```.WriteTo.File("Logs/log.txt")```
            .CreateLogger();
    - Step 2: Register logger service in Controller, example ```PublishersController``` controller
        ```cs
        private readonly ILogger<PublishersController> _logger;

        public PublishersController(PublishersService publishersService, ILogger<PublishersController> logger)
        {
            _publishersService = publishersService;
            _logger = logger;
        }
        ```
    - Store log
        ```cs
        _logger.LogError(ex.Message);
        _logger.LogInformation(JsonSerializer.Serialize(_result));
        _logger.LogDebug("log info");
        ```
    - Edit file log name:
        Log.Logger = new LoggerConfiguration()
                    .WriteTo.File("Logs/log.txt", ```rollingInterval: RollingInterval.Day```)
                    .CreateLogger();
