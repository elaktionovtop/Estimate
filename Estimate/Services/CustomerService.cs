using Estimate.Data;
using Estimate.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estimate.Services
{
    public class CustomerService : CrudService<Customer>
    {
        public CustomerService(AppDbContext db) : base(db) { }

        //public override IEnumerable<Customer> GetAll()
        //    => _db.Customers;

        public override Customer Clone(Customer source) => new()
        {
            Id = source.Id,
            Name = source.Name,
            Phone = source.Phone,
            Email = source.Email,
            Description = source.Description
        };

        public override void CopyTo(Customer source, Customer target)
        {
            target.Name = source.Name;
            target.Phone = source.Phone;
            target.Email = source.Email;
            target.Description = source.Description;
        }

        public override void Validate(Customer customer)
        {
            if(string.IsNullOrWhiteSpace(customer.Name))
                throw new ArgumentException("Имя клиента обязательно");

            if(string.IsNullOrWhiteSpace(customer.Phone))
                throw new ArgumentException("Телефон клиента обязателен");
        }

        protected override string GetDeleteErrorMessage
            (Customer customer) => "Невозможно удалить клиента: " 
            + "он связан с заказами.";
    }
}
