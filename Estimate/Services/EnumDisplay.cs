using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estimate.Services
{
    public class EnumDisplay<T>
    {
        public T Value { get; set; }
        public string Display { get; set; }

        public EnumDisplay(T value, string display)
        {
            Value = value;
            Display = display;
        }

        public override string ToString() => Display;
    }
}
