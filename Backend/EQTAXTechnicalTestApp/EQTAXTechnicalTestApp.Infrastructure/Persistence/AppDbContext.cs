using Microsoft.EntityFrameworkCore;

namespace EQTAXTechnicalTestApp.Infrastructure.Persistence
{
    /// <summary>
    /// Represents the application's database context used to interact with the underlying database
    /// through Entity Framework Core.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class using the specified options.
        /// These options are typically configured during application startup.
        /// </summary>
        /// <param name="options">
        /// The options to be used by this <see cref="DbContext"/>. These include the connection string,
        /// database provider, and other EF Core configuration settings.
        /// </param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
