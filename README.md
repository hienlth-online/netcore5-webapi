# netcore5-webapi
Demo .NET Core 5 (NET 5) Web API


# Demo 06: Web API Logging with Serilog
1. Install Serilog package
    - ```Serilog.AspNetCore```
    - ```Serilog.Sinks.File``` for store log in file
    - ```Serilog.Sinks.MSSqlServer``` for store log in database
2. Setup logging in ```Program.cs```:
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
    - In ```CreateHostBuilder``` method, config to save log:
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                ```.UseSerilog()```
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
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
4. Store log in text file - read from appsettings.json
    - Add Serilog config in appsettings.json
        ```json
        "Serilog": {
        "MinimumLevel": {
          "Default": "Information",
          "Override": {
            "System": "Error",
            "Microsoft": "Error"
          }
        },
        "WriteTo": [
          {
            "Name": "File",
            "Args": {
              "path": "Logs/log.txt",
              "rollingInterval": "Day",
              "outputTemplate": "{Timestamp} [{Level}] - Message: {Message}{NewLine}{Exception}"
            }
          }
        ]
      },
        ```
    - Config log in ```Main``` method, in ```Program.cs``` file.
        ```cs
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
        ```
5. Store log to SQL 
    5.1 Setup database
    - Add entity ```Log``` table:
        ```cs
        public class Log
        {
            public int Id { get; set; }
            public string Message { get; set; }
            public string MessageTemplate { get; set; }
            public string Level { get; set; }
    
            public DateTime TimeStamp { get; set; }
            public string Exception { get; set; }
            public string Properties { get; set; } //XML properties
            public string LogEvent { get; set; }
        }
        ```
    - Add dataset Log into MyDbContext:
        ```cs
        public DbSet<Log> Logs { get; set; }
        ```
    - Add migration and update database
    
    5.2 Config log
    - Add config as below into ```WriteTo``` section in ```appsettings.json``` file
        ```json
        {
            "Name": "MSSqlServer",
            "Args": {
              "connectionString": "Data Source=.;Initial Catalog=ASP5WebAPI_Book;Integrated Security=True;Pooling=False",
              "tableName": "Logs"
            }
        }
      ```
    - Run API app and see log in database
    
    5.3 Config LoggerFactory to handle all exceptions in Web APIs app.
    