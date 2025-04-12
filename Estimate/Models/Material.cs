using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estimate.Models
{
    public class Material
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }

        public MeasureUnit MeasureUnit { get; set; }
    }
}
