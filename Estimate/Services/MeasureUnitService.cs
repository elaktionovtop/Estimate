using Estimate.Data;
using Estimate.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estimate.Services
{
    public class MeasureUnitService : CrudService<MeasureUnit>
    {
        public MeasureUnitService(AppDbContext db) : base(db) { }

        public override MeasureUnit Clone(MeasureUnit source) => new()
        {
            Id = source.Id,
            Name = source.Name,
        };

        public override void CopyTo(MeasureUnit source, MeasureUnit target)
        {
            target.Name = source.Name;
        }

        public override void Validate(MeasureUnit item)
        {
            if(string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException("Имя клиента обязательно");
        }

        protected override string GetDeleteErrorMessage
            (MeasureUnit item) => "Невозможно удалить единицу измерения: "
            + "она связана с другими записями.";
    }
}

