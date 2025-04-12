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
    public abstract class TestBase
    {
        // User
        public const string AdminLogin = "admin";
        public const string AdminPassword = "123";
        public const Role AdminRole = Role.Admin;

        public const string EmployeeLogin = "employee_1";
        public const string EmployeePassword = "111";
        public const Role EmployeeRole = Role.Manager;

        // Employee
        public const int EmployeeUserId = 1;
        public const string EmployeeName = "EmployeeName_1";
        public const string EmployeePosition = "Manager";
        public const string EmployeePhone = "111111";

        // Customer
        public const string CustomerName = "CustomerName_1";
        public const string CustomerPhone = "222222";
        public const string CustomerEmail = "customer@gmail.com";

        // Construction
        public const string ConstructionName = "ConstructionName_1";
        public const string ConstructionAddress = "ConstructionAddress_1";

        // Unit
        public const string WorkUnitName = "WorkUnitName_1";
        public const string MaterialUnitName = "MaterialUnitName_1";

        // Work
        public const int WorkUnitId = 1;
        public const string WorkName = "WorkName_1";
        public const decimal WorkPrice = 500;   
        public const string WorkDescription = "WorkDescription_1";

        // Material
        public const int MaterialUnitId = 2;
        public const string MaterialName = "MaterialName_1";
        public const decimal MaterialPrice = 200;
        public const string MaterialDescription = "MaterialDescription_1";

        // Order
        public const int OrderCustomerId = 1;
        public const int OrderEmployeeId = 1;
        public const int OrderConstractionId = 1;
        public const string OrderDescription = "OrderDescription_1";

        // OrderWork
        public const int OrderWorkOrderId = 1;
        public const int OrderWorkWorkId = 1;
        public const int OrderWorkQuantity = 2;
        public const decimal OrderWorkPrice = 500;

        protected AppDbContext CreateTestContext()
        {
            AppDbContext db = new AppDbContext
                (new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid()
                .ToString())
                .Options);

            InitDb(db);
            db.SaveChanges();

            return db;
        }

        protected void SeedData(AppDbContext context, 
            Action<AppDbContext> action)
        {
            action(context);
            context.SaveChanges();
        }

        public static void InitDb(AppDbContext db)
        {
            db.Users.Add(new AppUser        // User.Id = 1 (Admin) !!!
            {
                Login = AdminLogin,
                Password = AdminPassword,
                Role = AdminRole
            });
            db.Users.Add(new AppUser        // User.Id = 2 (Employee) !!!
            {
                Login = EmployeeLogin,
                Password = EmployeePassword,
                Role = EmployeeRole
            });
            db.Employees.Add(new Employee
            {
                Name = EmployeeName,
                Position = EmployeePosition,
                Phone = EmployeePhone
            });
            db.Customers.Add(new Customer
            {
                Name = CustomerName,
                Phone = CustomerPhone,
                Email = CustomerEmail
            });
            db.Constructions.Add(new Construction
            {
                Name = ConstructionName,
                Address = ConstructionAddress
            });
            db.MeasureUnits.Add(new MeasureUnit  // MeasureUnits.Id = 1 (Work) !!!
            {
                Name = WorkUnitName
            });
            db.MeasureUnits.Add(new MeasureUnit  // MeasureUnits.Id = 2 (Materis) !!!
            {
                Name = MaterialUnitName
            });
            db.Works.Add(new Work 
            {
                UnitId = 1,                     // MeasureUnits.Id = 1 (Work) !!!
                Name = WorkName,
                Price = WorkPrice,
                Description = WorkDescription
            });
            db.Materials.Add(new Material
            {
                UnitId = 2,                     // MeasureUnits.Id = 2 (Materis) !!!
                Name = MaterialName,
                Price = MaterialPrice,
                Description = MaterialDescription
            });
        }
    }
}
