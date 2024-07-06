using Microsoft.EntityFrameworkCore;
using Personal_Blog_API.Models;

namespace Personal_Blog_API
{
    public class AppBbContext:DbContext
    {
        public DbSet<Article> Articles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var confing = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var ConnectionString = confing.GetSection("constr").Value;
            optionsBuilder.UseSqlServer(ConnectionString);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppBbContext).Assembly);
        }
    }
}
