using Estimate.Data;
using Estimate.Models;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estimate.Services
{
    public class EmployeeService : CrudService<Employee>
    {
        public EmployeeService(AppDbContext db) : base(db) { }

        public override IEnumerable<Employee> GetAll() 
            => _db.Employees;

        public override Employee Clone(Employee source) => new()
        {
            Id = source.Id,
            Name = source.Name,
            Position = source.Position,
            Phone = source.Phone,
            Description = source.Description
        };

        public override void CopyTo(Employee source, Employee target)
        {
            target.Name = source.Name;
            target.Position = source.Position;
            target.Phone = source.Phone;
            target.Description = source.Description;
        }

        public override void Validate(Employee employee)
        {
            if(string.IsNullOrWhiteSpace(employee.Name))
                throw new ArgumentException("Имя сотрудника обязательно");

            if(string.IsNullOrWhiteSpace(employee.Position))
                throw new ArgumentException("Должность сотрудника обязательна");

            if(string.IsNullOrWhiteSpace(employee.Phone))
                throw new ArgumentException("Телефон сотрудника обязателен");
        }

        protected override string GetDeleteErrorMessage
            (Employee employee) => "Невозможно удалить сотрудника: "
            + "он используется в заказах.";
    }
}
