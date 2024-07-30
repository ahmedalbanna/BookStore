using Bookstore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration; // Add this namespace

namespace BookStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; } // Inject IConfiguration

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookstoreDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BookstoreConnection")));

            // ... other services
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false; // for legacy MVC projects
                //options.EnableClientSideValidators = true;
            });

        }

        // ... other methods

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DbInitializer dbInitializer)
        {
            // ... other configuration logic

            // Seed the database
            dbInitializer.Initialize();

            // ... rest of your Configure method
        }
    }
}