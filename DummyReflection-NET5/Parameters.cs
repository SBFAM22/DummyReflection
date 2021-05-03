using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyReflection
{
    public class Parameters
    {
        public Type[] Constructor { get; set; } = new Type[] { };
        public object[] Arguments { get; set; } = new object[] { };

        public static Parameters Empty = new Parameters { Constructor = new Type[] { }, Arguments = new object[] { } };
    }

}
