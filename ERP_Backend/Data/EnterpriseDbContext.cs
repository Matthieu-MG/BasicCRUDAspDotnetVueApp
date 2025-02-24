using Enterprise.Models;
using Microsoft.EntityFrameworkCore;

namespace Enterprise.Data
{
    public class EnterpriseDbContext: DbContext
    {
        public EnterpriseDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Quotation> Quotations { get; set; } = null!;
        public DbSet<Order> Order { get; set; } = null!;

        public DbSet<Product> Products{ get; set; } = null!;
        public DbSet<Society> Society { get; set;} = null!;
        public DbSet<Employee> Employee {get; set;} = null!;

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");

            configurationBuilder.Properties<DateOnly?>()
                .HaveConversion<NullableDateOnlyConverter>()
                .HaveColumnType("date");
        }
    }
}