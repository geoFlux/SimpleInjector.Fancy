using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Funky.SimpleInjector
{
    public static class Extensions
    {
        /// <summary>
        /// Register a function to create an instance for type T given a consumer of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>        
        /// <param name="canCreateInstance">return true if instanceCreator Func is capable of returning an instance</param>
        /// <param name="instanceCreator">return an instance of type T, based on the consumer of type T</param>
        public static void RegisterSingleton<T>(this Container _this, Func<InjectionConsumerInfo, bool> canCreateInstance, Func<InjectionConsumerInfo, T> instanceCreator) where T : class
        {
            _this.Options.DependencyInjectionBehavior = new FancyInjectionBehavior<T>(_this, canCreateInstance, instanceCreator, info => info, Lifestyle.Singleton);
        }
        public static void Register<T>(this Container _this, Func<InjectionConsumerInfo, bool> canCreateInstance, Func<InjectionConsumerInfo, T> instanceCreator, Func<InjectionConsumerInfo, object> keyFunc, Lifestyle lifestyle) where T : class
        {
            _this.Options.DependencyInjectionBehavior = new FancyInjectionBehavior<T>(_this, canCreateInstance, instanceCreator, keyFunc, lifestyle);
        }

        public static void RegisterByParameterNames<T>(this Container _this, Lifestyle lifestyle, params Func<string, T>[] funcs) where T : class
        {
            var funcTable = getNamedFuncs(funcs);

            Register(_this,
                info => funcTable.Keys.Contains(info.Target.Parameter.Name),
                info => funcTable[info.Target.Parameter.Name](info.Target.Parameter.Name),
                info => info.Target.Parameter.Name,
                lifestyle);
        }

        static Dictionary<string, Func<string, T>> getNamedFuncs<T>(params Func<string, T>[] funcs)
        {
            return funcs
                .ToDictionary(f => f.Method.GetParameters().First().Name);
        }
    }
}
