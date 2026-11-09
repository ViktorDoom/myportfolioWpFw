namespace myportfolio;
using Microsoft.EntityFrameworkCore;
using data;
using services;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.AddControllers(); 
        
        //swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        // Retrieve the database connection string
        var connectionString = builder.Configuration
                                   .GetConnectionString("DefaultConnection")
                               ?? throw new InvalidOperationException(
                                   "Connection string 'DefaultConnection' not found.");
        
        builder.Services.AddDbContext<PortfolioDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        
        // Dep injectie
        builder.Services.AddScoped<IContactRequestService, ContactRequestService>();
        builder.Services.AddScoped<IProjectsService, ProjectService>();
        builder.Services.AddScoped<IBlogPostsService,  BlogPostsService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.MapControllers();
        app.Run();
    }
}