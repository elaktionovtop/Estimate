using Estimate.Data;
using Estimate.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimateTest
{
    public class TestBase
    {
        public static AppDbContext CreateContext()
        {
            AppDbContext db = new AppDbContext
                (new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid()
                .ToString())
                .Options);
            return db;
        }
    }
}
