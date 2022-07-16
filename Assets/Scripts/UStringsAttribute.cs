using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMoltkoff.UStrings
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class UStringsAttribute : Attribute
    {
        public string[] Values { get; set; }

        public UStringsAttribute(params string[] values)
        {
            this.Values = values;
        }
    }
}