using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using System.Windows;

using Estimate.Models;

namespace Estimate.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<AppUser> Users => Set<AppUser>();
        public DbSet<Order> Orders => Set<Order>();
        // Остальные сущности...

        public AppDbContext()
            : base()
        { }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database\\EstimateDB.mdf");
    }
}
