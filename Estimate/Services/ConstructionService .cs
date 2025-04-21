using Estimate.Data;
using Estimate.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estimate.Services
{
    public class ConstructionService : CrudService<Construction>
    {
        public ConstructionService(AppDbContext db) : base(db) { }

        public override IEnumerable<Construction> GetAll() 
            => _db.Constructions;

        public override Construction Clone
            (Construction source) => new()
        {
            Id = source.Id,
            Name = source.Name,
            Address = source.Address,
            Description = source.Description
        };

        public override void CopyTo(Construction source, Construction target)
        {
            target.Name = source.Name;
            target.Address = source.Address;
            target.Description = source.Description;
        }

        public override void Validate(Construction construction)
        {
            if(string.IsNullOrWhiteSpace(construction.Name))
                throw new ArgumentException
                    ("Название объекта обязательно");

            if(string.IsNullOrWhiteSpace(construction.Address))
                throw new ArgumentException("Адрес объекта обязателен");
        }

        protected override string GetDeleteErrorMessage
            (Construction construction) => "Невозможно удалить объект: "
            + "он используется в заказах.";
    }
}
