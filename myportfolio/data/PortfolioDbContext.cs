namespace myportfolio.data;
using Microsoft.EntityFrameworkCore;
using model;


public class PortfolioDbContext: DbContext
{
    public PortfolioDbContext(
        DbContextOptions<PortfolioDbContext> options)
        : base(options)
    {
    }
    public DbSet<Project> Projects {get; set;}
    public DbSet<BlogPost> BlogPosts {get; set;}
    public DbSet<ContactForm>  ContactForms {get; set;}
    
}