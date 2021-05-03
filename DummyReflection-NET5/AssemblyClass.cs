using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DummyReflection
{
    public record AssemblyClass
    {
        public AssemblyClass(Type t, Parameters parameters)
        {
            CurrentType = t;

            if (CurrentType is null)
                throw new Exception("Your Constructor Does Not Match Any Found In The Type!");

            ConstructorArgs = parameters.Arguments;
            ConstructorTypes = parameters.Constructor;

            Instance = CreateInstance();
        }

        /// <summary>
        /// Read DummyClass For More Info
        /// </summary>
        public DummyClass Dummy { get { return new DummyClass(CurrentType, Instance); } }

        public Type CurrentType { get; }

        /// <summary>
        /// The Arguments Supplied For The CTOR Used 
        /// </summary>
        public object[] ConstructorArgs { get; }

        /// <summary>
        /// The Types Supplied For The CTOR Used
        /// </summary>
        public Type[] ConstructorTypes { get; }

        /// <summary>
        /// New Instance Of The Type
        /// <br></br>
        /// <br>Do Not Use For Attributes -- Use Dummy</br>
        /// <br>Can Be Changed To Your Own Instance But Uses The Instance Made By Your CTOR</br>
        /// <br>Note: Can Be Used For Static Methods/Variables In Types</br>
        /// </summary>
        public object Instance { get; set; }

        /// <summary>
        /// Creates A New Instance Of The Type
        /// </summary>
        /// <returns></returns>
        public object CreateInstance()
        {
            return DummyReflect.FindConstructor(CurrentType, ConstructorTypes).Invoke(ConstructorArgs);
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
                if (CurrentType.GetCustomAttribute(z, inherits) != null)
                    return new FoundAttribute(z, CurrentType.GetCustomAttribute(z, inherits));

            return default;
        }
    }
}
