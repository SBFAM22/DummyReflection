using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DummyReflection
{
    public record Variable
    {
        public bool IsField { get { return Info?.MemberType == MemberTypes.Field; } }
        public bool IsProp { get { return !IsField; } }

        /// <summary>
        /// The Value That The Field/Property Returns When Getting The Value
        /// </summary>
        public object ValueType { get; }

        /// <summary>
        /// Info On The Member
        /// </summary>
        public MemberInfo Info { get; }

        /// <summary>
        /// Instance You Gave
        /// </summary>
        public object Instance { get; }

        public Variable(MemberInfo info, object instance)
        {
            Info = info;
            Instance = instance;
            ValueType = GetValue();
        }


        /// <summary>
        /// Sets The Value Of The Field/Property (<typeparamref name="T"/>)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        public void SetValue<T>(T val)
        {
            if (IsProp)
            {
                PropertyInfo info = (PropertyInfo)Info;

                if (info.SetMethod == null)
                    return;

                info.SetValue(Instance, val);
            }

            if (IsField)
            {
                FieldInfo info = (FieldInfo)Info;

                if (info.IsLiteral)
                    return;

                info.SetValue(Instance, val);
            }
        }

        /// <summary>
        /// Sets The Value Of The Field/Property
        /// </summary>
        /// <param name="val"></param>
        public void SetValue(object val)
        {
            if (IsProp)
            {
                PropertyInfo info = (PropertyInfo)Info;

                if (info.SetMethod == null)
                    return;

                info.SetValue(Instance, val);
            }

            if (IsField)
            {
                FieldInfo info = (FieldInfo)Info;

                if (info.IsLiteral)
                    return;

                info.SetValue(Instance, val);
            }
        }

        /// <summary>
        /// Returns The Value Of The Field/Property(<typeparamref name="T"/>)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetValue<T>()
        {
            if (IsProp)
            {
                PropertyInfo info = (PropertyInfo)Info;

                return (T)info?.GetValue(Instance);
            }
            else
            {
                FieldInfo info = (FieldInfo)Info;
                return (T)info?.GetValue(Instance);
            }
        }

        /// <summary>
        /// Returns The Value Of The Field/Property(<see cref="object"/>)
        /// </summary>
        /// <returns></returns>
        public object GetValue()
        {
            if (IsProp)
            {
                PropertyInfo info = (PropertyInfo)Info;

                return info.GetValue(Instance);
            }
            else
            {
                FieldInfo info = (FieldInfo)Info;
                return info.GetValue(Instance);
            }
        }

        /// <summary>
        /// <paramref name="constructor"/> Finds The Specific Attribute CTOR
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
