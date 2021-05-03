using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DummyReflection
{
    public record FoundConstructor
    {
        public FoundConstructor(ConstructorInfo info)
        {
            Info = info;

            if (Info is not null)
                Params = info.GetParameters();
            else
                throw new Exception("CTOR Is Null");
        }

        /// <summary>
        /// Parameters On The CTOR
        /// </summary>
        public ParameterInfo[] Params { get; set; }
        /// <summary>
        /// Info on the CTOR
        /// </summary>
        public ConstructorInfo Info { get; }


        private BindingFlags All = (BindingFlags)(-1);

        /// <summary>
        /// Invokes The CTOR
        /// </summary>
        /// <returns></returns>
        public object Invoke(params object[] args)
        {
            if (Info is null)
                throw new Exception("Constructor Was Null");

            return Info.Invoke(All, null, args, null);
        }

        /// <summary>
        /// Invokes The CTOR
        /// </summary>
        /// <returns></returns>
        public T Invoke<T>(params object[] args)
        {
            if (Info is null)
                throw new Exception("Constructor Was Null");

            return (T)Info.Invoke(All, null, args, null);
        }

    }
}
