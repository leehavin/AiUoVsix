using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AiUoVsix.Common
{
    public class EnumItem : IComparer<EnumItem>
    {
        public int Value { get; set; }

        public string Name { get; set; }

        public string MapName { get; set; }

        public string Description { get; set; }

        public FieldInfo FieldInfo { get; set; }

        public int Compare(EnumItem x, EnumItem y)
        {
            return x.Value.CompareTo(y.Value);
        }
    }
}
