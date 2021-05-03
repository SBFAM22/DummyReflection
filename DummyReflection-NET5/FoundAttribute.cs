using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyReflection
{
    public record FoundAttribute(Type InstanceType, object Instance)
    {
        /// <summary>
        /// <paramref name="name"/> Should Be The Name Of The Field/Property That Is Assigned To The Value Inside Of The CTOR
        /// <br>Returns Null If That Value Is Never Set</br>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public object GetValue(string name)
        {
            return Instance.GetVariable(name).GetValue();
        }

        /// <summary>
        /// <paramref name="name"/> Should Be The Name Of The Field/Property That Is Assigned To The Value Inside Of The CTOR
        /// <br>Returns Null If That Value Is Never Set</br>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T GetValue<T>(string name)
        {
            return Instance.GetVariable(name).GetValue<T>();
        }
    }
}
