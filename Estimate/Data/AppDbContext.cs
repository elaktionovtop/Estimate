using Microsoft.EntityFrameworkCore;

using Estimate.Models;

namespace Estimate.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<AppUser> Users => Set<AppUser>();
        public DbSet<Employee> Employees => Set<Employee>();

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Construction> Constructions => Set<Construction>();
        public DbSet<MeasureUnit> MeasureUnits => Set<MeasureUnit>();
        public DbSet<Work> Works => Set<Work>();
        public DbSet<Material> Materials => Set<Material>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderWork> OrderWorks => Set<OrderWork>();
        public DbSet<OrderMaterial> OrderMaterials => Set<OrderMaterial>();

        public AppDbContext()
            : base()
        { }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { 
        }

        public void Load()
        {
            Users.Load();
            Employees.Load();

            Works.Include(o => o.MeasureUnit).Load();
            OrderWorks.Include(o => o.Work).Load();
            Orders.Include(o => o.Materials).Include(o => o.Works).Load();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database\\EstimateDB.mdf");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // OrderWork: каждый Order может иметь много строк (работ)
            modelBuilder.Entity<OrderWork>()
                .HasOne(ow => ow.Order)
                .WithMany(o => o.Works)
                .HasForeignKey(ow => ow.OrderId)
                .OnDelete(DeleteBehavior.Restrict); // нельзя удалить Order, если есть строки

            modelBuilder.Entity<OrderWork>()
                .HasOne(ow => ow.Work)
                .WithMany()
                .HasForeignKey(ow => ow.WorkId)
                .OnDelete(DeleteBehavior.Restrict); // нельзя удалить Work, если он использован

            // OrderMaterial: аналогично
            modelBuilder.Entity<OrderMaterial>()
                .HasOne(om => om.Order)
                .WithMany(o => o.Materials)
                .HasForeignKey(om => om.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderMaterial>()
                .HasOne(om => om.Material)
                .WithMany()
                .HasForeignKey(om => om.MaterialId)
                .OnDelete(DeleteBehavior.Restrict);

            //// Employee → User (один пользователь может быть привязан к сотруднику)
            //modelBuilder.Entity<Employee>()
            //    .HasOne(e => e.)
            //    .WithMany()
            //    .HasForeignKey(e => e.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            // Material → MeasureUnit
            modelBuilder.Entity<Material>()
                .HasOne(m => m.MeasureUnit)
                .WithMany()
                .HasForeignKey(m => m.UnitId)
                .OnDelete(DeleteBehavior.Restrict);

            // Work → MeasureUnit
            modelBuilder.Entity<Work>()
                .HasOne(w => w.MeasureUnit)
                .WithMany()
                .HasForeignKey(w => w.UnitId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        //public class ContextFactory :
        //    IDesignTimeDbContextFactory<AppDbContext>
        //{
        //    public AppDbContext CreateDbContext(string[] args)
        //    {
        //        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        //        ConfigurationBuilder configurationBuilder = new();
        //        configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
        //        configurationBuilder.AddJsonFile("config.json");
        //        IConfigurationRoot config = configurationBuilder.Build();

        //        string connectionString = config.GetConnectionString("DefaultConnection");
        //        optionsBuilder.UseSqlServer(connectionString);
        //        return new AppDbContext(optionsBuilder.Options);
        //    }
    }
}
