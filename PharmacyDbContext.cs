using System.Data.Entity;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Data
{
    /// <summary>
    /// Entity Framework DbContext for the Pharmacy Management System.
    /// Uses connection string "PharmacyDbContext" from App.config.
    /// Includes DbSet properties for all model entities.
    /// </summary>
    public class PharmacyDbContext : DbContext
    {
        public PharmacyDbContext()
            : base("name=PharmacyDbContext")
        {
            // Disable automatic database creation/migration.
            // The database must be created manually using the SQL script.
            Database.SetInitializer<PharmacyDbContext>(null);
        }

        // -------------------------
        // DbSet properties
        // -------------------------
        public DbSet<Admin>    Admins    { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sale>     Sales     { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map entities to exact table names in the database.
            modelBuilder.Entity<Admin>().ToTable("Admins");
            modelBuilder.Entity<Medicine>().ToTable("Medicines");
            modelBuilder.Entity<Supplier>().ToTable("Suppliers");
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Sale>().ToTable("Sales");

            // Configure Sale -> Medicine foreign key
            modelBuilder.Entity<Sale>()
                .HasRequired(s => s.Medicine)
                .WithMany(m => m.Sales)
                .HasForeignKey(s => s.MedicineId);

            // Configure Sale -> Customer foreign key
            modelBuilder.Entity<Sale>()
                .HasRequired(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CustomerId);
        }
    }
}
