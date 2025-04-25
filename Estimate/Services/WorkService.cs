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
    public class WorkService : CrudService<Work>
    {
        public WorkService(AppDbContext db) : base(db) { }

        public override IEnumerable<Work> GetAll()
            => _db.Works
                .Include(w => w.MeasureUnit)
                ;

        public override Work Clone(Work source)
        {
            return new Work
            {
                Id = source.Id,
                UnitId = source.UnitId,
                Name = source.Name,
                Price = source.Price,
                Description = source.Description,

                MeasureUnit = source.MeasureUnit
            };
        }

        public override void CopyTo(Work source, Work target)
        {
            target.UnitId = source.UnitId;
            target.Name = source.Name;
            target.Price = source.Price;
            target.Description = source.Description;

            target.MeasureUnit = source.MeasureUnit;
        }

        // можно проверить ссылки Customver и т.д.
        public override void Validate(Work work)
        {
            if(work.UnitId == 0)
                throw new ArgumentException("Единица измерения не указана");

            if(string.IsNullOrWhiteSpace(work.Name))
                throw new ArgumentException("Наименование  работы обязательно");

            if(work.Price <= 0)
                throw new ArgumentException("Цена должна быть положительной");

        }

        protected override string GetDeleteErrorMessage(Work work)
            => "Невозможно удалить вид работы: "
            + "он содержит связанные работы заказов.";
    }
}

