# netcore5-webapi
Demo .NET Core 5 (NET 5) Web API


# Demo 02 - Relationship
## Some steps to action
1. Create new Model, named ```Publisher``` that related to ```Book```
2. Create new Model, named Books
3. Create new DbContext, named MyDBContext
4. Install package: EF Core
	- ```Microsoft.EntityFrameworkCore```, version 5.0.9
	- ```Microsoft.EntityFrameworkCore.SqlServer```, version 5.0.9
	- ```Microsoft.EntityFrameworkCore.Tools```, version 5.0.9 (for PMC Migration)
5. Create connection string: Open appsettings.json, add new ConnectionString
    ```json
    "ConnectionStrings": {
        "DefaultConnectionString": "Server=.; Database=ASP5WebAPI_Book; Integrated Security=True;"
    }
    ```
6. Open Startup, register DbContext service
    ```cs
    services.AddDbContext<MyDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));
    ```
7. Create Migration
    - Open Package Manager Console (PMC)
    - Type ```Add-Migration InitDB```
    - Type ```Update-Database```
8. Create Seed Data
    - Create class with static method with parameter ```IApplicationBuilder``` to create default data
        ```cs
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<MyDbContext>();

                if (!context.Books.Any())
                {
                    context.Books.AddRange(new Book()
                    {
                        Title = "ASP.NET Core 5",
                        Description = "1st Book Description",
                        IsRead = true,
                        DateRead = DateTime.Now.AddDays(-10),
                        Rate = 4,
                        Genre = "Biography",
                        Author = "HIENLTH",
                        CoverUrl = "https....",
                        DateAdded = DateTime.Now
                    },
                    new Book()
                    {
                        Title = "Entity Framework Core 5",
                        Description = "2nd Book Description",
                        IsRead = false,
                        Genre = "Biography",
                        Author = "HIENLTH",
                        CoverUrl = "https....",
                        DateAdded = DateTime.Now
                    });

                    context.SaveChanges();
                }
            }
        }
        ```
    - Call Seed method in StartUp
        ```MyDbInitialer.Seed(app);```
9. Add new API controller, called ```BooksController```
10. Create ViewModels folder and add new ```BookVM``` class
11. Create and register a ```BooksService```
    - Consider automatic mapping ```BookVM``` and ```Book``` model.
    - In ```StartUp``` class, register this service.
        ```cs
        services.AddTransient<BooksService>();
        ```
12. Back to ```BooksController```, write some APIs:
    - API POST to create new Book
    - GET all books
    - GET book by id
    - API PUT to update an existing book
    - DELETE an existing book