// Importing necessary namespaces
using Microsoft.EntityFrameworkCore; // Provides Entity Framework Core functionalities for database interaction
using TodoAPi.Models; // Imports the Todoitems model used in the application

// Defining the namespace for better project structuring
namespace TodoAPi.Context
{
    // Defining the database context class that inherits from DbContext
    public class ToDODbcontext : DbContext
    {
        // Constructor to initialize the DbContext with specific options
        public ToDODbcontext(DbContextOptions<ToDODbcontext> options) : base(options)
        {
        }

        // Defining a DbSet property for the Todoitems table, representing the database entity
        public DbSet<Todoitems> Todoitems { get; set; }

        // Configuring entity relationships and additional configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Calling the base class implementation of OnModelCreating
            base.OnModelCreating(modelBuilder);

            // Dynamically applies all entity configurations from the current assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDODbcontext).Assembly);
        }
    }
}
