using CBZ_OverTime_Logging.Models;
using CBZ_OvertTime_Logging.Models;
using Microsoft.EntityFrameworkCore;

namespace CBZ_OvertTime_Logging.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Subsidiary> Subsidiaries { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Units> Units { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<OvertimeClaim> OvertimeClaims { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<vw_OverTimeClaims> vw_OverTimeClaims { get; set; }
        public DbSet<vw_Employees> vw_Employees { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Subsidiary
            modelBuilder.Entity<Subsidiary>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Subsidiary>()
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Configure Department
            modelBuilder.Entity<Department>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<Department>()
                .Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Department>()
                .HasOne<Subsidiary>()
                .WithMany()
                .HasForeignKey(d => d.SubsidiaryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Department
            modelBuilder.Entity<Units>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<Units>()
                .Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Units>()
                .HasOne<Department>()
                .WithMany()
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Employee
            modelBuilder.Entity<Employees>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Employees>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Employees>()
                .HasOne<Units>()
                .WithMany()
                .HasForeignKey(e => e.UnitId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure OvertimeClaim
            modelBuilder.Entity<OvertimeClaim>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<OvertimeClaim>()
                .Property(o => o.Reason)
                .IsRequired()
                .HasMaxLength(500);

            modelBuilder.Entity<OvertimeClaim>()
                .HasOne<Employees>()
                .WithMany()
                .HasForeignKey(o => o.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Additional configurations (if needed)
            modelBuilder.Entity<OvertimeClaim>()
                .Property(o => o.StartDateTime)
                .IsRequired();

            modelBuilder.Entity<OvertimeClaim>()
                .Property(o => o.EndDateTime)
                .IsRequired();

            modelBuilder.Entity<OvertimeClaim>()
                .Property(o => o.OvertimeHours)
                .HasPrecision(18, 2); // Set precision for decimal fields

            modelBuilder.Entity<OvertimeClaim>()
                .Property(o => o.Rate)
                .HasPrecision(18, 2); // Set precision for decimal fields
                                      // Configure Rates
            modelBuilder.Entity<Rate>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Rate>()
                .Property(r => r.Type)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Rate>()
                .Property(r => r.Amount)
                .IsRequired()
                .HasPrecision(18, 2);

            // Configure Holidays
            modelBuilder.Entity<Holiday>()
                .HasKey(h => h.Id);

            modelBuilder.Entity<Holiday>()
                .Property(h => h.Date)
                .IsRequired();

            modelBuilder.Entity<Holiday>()
                .Property(h => h.Description)
                .HasMaxLength(100);

            base.OnModelCreating(modelBuilder);

        }
    }
}
