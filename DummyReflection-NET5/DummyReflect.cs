using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DummyReflection
{
    public static class DummyReflect
    {
        private static BindingFlags All = (BindingFlags)(-1);

        /// <summary>
        /// Get A Field/Property By Name From This Instance
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Variable GetVariable(this object instance, string name)
        {
            return new Variable(GetMember(instance, name), instance) { };
        }

        private static MemberInfo GetMember(object instance, string name)
        {
            Type t = instance.GetType();

            if (t.GetField(name, All) != null)
                return t.GetField(name, All);
            else if (t.GetProperty(name, All) != null)
                return t.GetProperty(name, All);
            else
                return default;
        }

        /// <summary>
        /// Get A Method From This Instance By <paramref name="name"/>, Specify The Method By <see cref="Type"/>[] <paramref name="parameters"/>
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Method GetMethod(this object instance, string name, params Type[] parameters)
        {
            Type t = instance.GetType();

            var info = t.GetMethod(name, All, null, parameters, null);

            return new Method(info, instance);
        }

        /// <summary>
        /// This FindType uses an Assembly and Makes It Easier To Get A Type
        /// <br></br>
        /// <br>Note: Does Not Include Current Instance</br>
        /// </summary>
        /// <param name="assem"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static AssemblyClass FindType(this Assembly assem, string name, Parameters parameters = null)
        {
            if (parameters == null)
                parameters = Parameters.Empty;

            var ret = assem?.GetTypes().Where(x => x.Name == name && x.GetConstructor(All, null, parameters.Constructor, null) != null).FirstOrDefault();

            return new AssemblyClass(ret, parameters);
        }

        /// <summary>
        /// This FindType Is Made For Finding a Type based of a T type and Name
        /// <br> </br>
        /// <br>The <typeparamref name="TypeFromAssem"/> Should Be Fed Your typeof(T) a Type Inside Of The Assembly You Want</br>
        /// <br>Note: Does Not Include Current Instance</br>
        /// </summary>
        /// <typeparam name="TypeFromAssem"></typeparam>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static AssemblyClass FindType<TypeFromAssem>(string name, Parameters parameters = null)
        {
            if (parameters == null)
                parameters = Parameters.Empty;

            var ret = typeof(TypeFromAssem).Assembly.GetTypes().Where(x => x.Name == name && x.GetConstructor(All, null, parameters.Constructor, null) != null).FirstOrDefault();

            return new AssemblyClass(ret, parameters);
        }

        /// <summary>
        /// This FindType Is Made For Instances Specifically
        /// <br></br>
        /// <br>Note: Does Not Include Current Instance</br>
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static AssemblyClass FindType(object instance, string name, Parameters parameters = null)
        {
            if (parameters == null)
                parameters = Parameters.Empty;

            var ret = instance.GetType().Assembly.GetTypes().Where(x => x.Name == name && x.GetConstructor(All, null, parameters.Constructor, null) != null).FirstOrDefault();

            return new AssemblyClass(ret, parameters);
        }

        public static FoundConstructor FindConstructor<T>(params Type[] constructor)
        {
            Type iT = typeof(T);

            var cInfo = iT.GetConstructor(All, null, constructor, null);

            if (cInfo is null)
                throw new Exception("Constructor Was Null");
            else
                return new FoundConstructor(cInfo);
        }

        public static FoundConstructor FindConstructor(Type t, params Type[] constructor)
        {
            Type iT = t;

            var cInfo = iT.GetConstructor(All, null, constructor, null);

            if (cInfo is null)
                throw new Exception("Constructor Was Null");
            else
                return new FoundConstructor(cInfo);
        }
    }
}
