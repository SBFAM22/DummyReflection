using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Reflection.Emit;

namespace DummyReflection
{
    public record Method(MethodInfo Info, object Instance)
    {
        /// <summary>
        /// Type the Method Returns After Invoked
        /// </summary>
        public Type ReturnType { get { return Info.ReturnType; } }

        /// <summary>
        /// Does The Method Return What You Want?
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool IsReturnType<T>()
        {
            if (ReturnType != null)
            {
                if (ReturnType == typeof(T))
                    return true;
                else
                    return false;
            }

            return false;
        }

        /// <summary>
        /// Calls The Method | Optional <paramref name="parameters"/> | Returns Object
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object Call(params object[] parameters)
        {
            return Info.Invoke(Instance, parameters);
        }

        /// <summary>
        /// Calls The Method | Optional <paramref name="parameters"/> | Returns <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T Call<T>(params object[] parameters)
        {
            object res = Info.Invoke(Instance, parameters);

            if (res == null)
                return default;

            return (T)res;
        }

        /// <summary>
        /// <paramref name="constructor"/> Is To Find Specific Constructors But Does Not Use Its Values
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="inherits"></param>
        /// <param name="constructor"></param>
        /// <returns></returns>
        public FoundAttribute GetAttribute(string attributeName, bool inherits, params Type[] constructor)
        {
            Type z = DummyReflect.FindType(Instance, attributeName, new Parameters { Arguments = null, Constructor = constructor }).CurrentType;

            if (z.IsSubclassOf(typeof(Attribute)))
                if (Info.GetCustomAttribute(z, inherits) != null)
                    return new FoundAttribute(z, Info.GetCustomAttribute(z, inherits));

            return default;
        }

    
    }
}
