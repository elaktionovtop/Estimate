using Estimate.Models;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estimate.Data
{
    public static class DbVerification
    {
        static AppDbContext _db;

        public static void Perform()
        {
            _db = App.Services.GetRequiredService<AppDbContext>();
            _db.Database.EnsureCreated();

            VerifyUsers();

            VerifyCustomers();
            VerifyEmployees();
            VerifyConstructions();
            VerifyOrders();
            VerifyMeasureUnits();
            VerifyWorks();

            _db.SaveChanges();
        }

        static void VerifyUsers()
        {
            if(!_db.Users.Any())
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
                        Password = "111",
                        Role = Role.Manager
                    }
                };
                _db.Users.AddRange(users);
            }
        }

        static void VerifyConstructions()
        {
            if(!_db.Constructions.Any())
            {
                var constructions = new Construction[]
                {
                    new Construction
                    {
                        Name = "Обьект 1",
                        Address = "Адрес 1"
                    },
                    new Construction
                    {
                        Name = "Обьект2",
                        Address = "Адрес 2"
                    }
                };
                _db.Constructions.AddRange(constructions);
            }
        }

        static void VerifyEmployees()
        {
            if(!_db.Employees.Any())
            {
                var employees = new Employee[]
                {
                    new Employee
                    {
                        Name = "Сотрудник 1",
                        Position = "Менеджер",
                        Phone = "111111"
                    },
                    new Employee
                    {
                        Name = "Сотрудник 2",
                        Position = "Менеджер",
                        Phone = "222222"
                    }
                };
                _db.Employees.AddRange(employees);
            }
        }

        static void VerifyCustomers()
        {
            if(!_db.Customers.Any())
            {
                var customers = new Customer[]
                {
                    new Customer
                    {
                        Name = "Заказчик 1",
                        Phone = "1111"
                    },
                    new Customer
                    {
                        Name = "Заказчик 2",
                        Phone = "2222"
                    }
                };
                _db.Customers.AddRange(customers);
            }
        }

        static void VerifyOrders()
        {
            if(!_db.Orders.Any())
            {
                var orders = new Order[]
                {
                    new Order
                    {
                        CustomerId = 1,
                        EmployeeId = 1,
                        ConstructionId = 1,
                        Description = "Заказ 1"
                    },
                    new Order
                    {
                        CustomerId = 2,
                        EmployeeId = 2,
                        ConstructionId = 2,
                        Description = "Заказ 2"
                    }
                };
                _db.Orders.AddRange(orders);
            }
        }

        static void VerifyMeasureUnits()
        {
            if(!_db.MeasureUnits.Any())
            {
                var measureUnits = new MeasureUnit[]
                {
                    new MeasureUnit
                    {
                        Name = "штук",
                    },
                    new MeasureUnit
                    {
                        Name = "кг",
                    }
                };
                _db.MeasureUnits.AddRange(measureUnits);
            }
        }

        static void VerifyWorks()
        {
            if(!_db.Works.Any())
            {
                var works = new Work[]
                {
                new Work
                {
                    UnitId = 1,
                    Name = "Ремонт_1",
                    Price = 1000,
                },
                new Work
                {
                    UnitId = 1,
                    Name = "Ремонт_2",
                    Price = 2000,
                }
                };
                _db.Works.AddRange(works);
            }
        }

        static void VerifyMaterials()
        {
            if(!_db.Materials.Any())
            {
                var materials = new Material[]
                {
                new Material
                {
                    UnitId = 2,
                    Name = "Материал_1",
                    Price = 1000,
                },
                new Material
                {
                    UnitId = 2,
                    Name = "Материал_2",
                    Price = 2000,
                }
                };
                _db.Materials.AddRange(materials);
            }
        }
    }
}
