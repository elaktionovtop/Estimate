using Estimate.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estimate.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated(); // Создаёт БД, если её нет

            if(!context.Users.Any())
            {
                var users = new AppUser[]
                {
                    new AppUser 
                    { 
                        Login = "admin", 
                        Password = "123", 
                        Role = Role.Admin 
                    },
                    new AppUser 
                    { 
                        Login = "manager", 
                        Password = "456", 
                        Role = Role.Manager 
                    }
                };
                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }
    }
}
