using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DummyReflection
{
    public record DummyClass
    {
        private BindingFlags All = (BindingFlags)(-1);

        /// <summary>
        /// The Type In Use At The Time
        /// </summary>
        public Type InUse { get; }

        /// <summary>
        /// Every Method Inside The Privated Class Under This Instance(Alphabetically Ordered)(Non-Special)
        /// </summary>
        public Method[] Methods { get; }

        /// <summary>
        /// Every Special-Method Inside The Privated Class Under This Instance(Alphabetically Ordered)(Special)
        /// </summary>
        public Method[] SpecialMethods { get; }

        /// <summary>
        /// Every Property and Field Inside The Type Under This Instance(Alphabetically Ordered)
        /// </summary>
        public Variable[] Variables { get; }

        /// <summary>
        /// Instance Of Type
        /// </summary>
        public object Instance { get; }

        public DummyClass(Type classToUse, object instance)
        {
            InUse = classToUse;
            Instance = instance;

            List<Method> methods = new List<Method>();
            List<Method> specialMethods = new List<Method>();
            foreach (var item in InUse.GetMethods(All))
                if (!item.IsSpecialName)
                    methods.Add(new Method(item, Instance));
                else
                    specialMethods.Add(new Method(item, Instance));

            List<Variable> vars = new List<Variable>();

            foreach (var item in InUse.GetFields(All))
                vars.Add(new Variable(item, Instance) { });

            foreach (var item in InUse.GetProperties(All))
                vars.Add(new Variable(item, Instance) { });

            Methods = methods.OrderBy(x => x.Info.Name).ToArray();
            Variables = vars.OrderBy(x => x.Info.Name).ToArray();
            SpecialMethods = specialMethods.OrderBy(x => x.Info.Name).ToArray();
        }
    }
}
