using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleInjector.Fancy
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
        public static void RegisterWithConstructorParams<TService, TImplementation, TParam1>(this Container _this, Lifestyle lifestyle, Func<string, TParam1> func1) where TParam1: class
        {
            var funcTable = getNamedFuncs(func1);
            Register(_this,
                info => funcTable.Keys.Contains(info.Target.Parameter.Name) && info.ImplementationType == typeof(TImplementation),
                info => funcTable[info.Target.Parameter.Name](info.Target.Parameter.Name),
                info => info.Target.Parameter.Name.GetHashCode()^ typeof(TImplementation).GetHashCode(),
                lifestyle);
        }
        public static void RegisterByParameterNames<T>(this Container _this, Lifestyle lifestyle, params Func<string, T>[] funcs) where T : class
        {
            var funcTable = getNamedFuncs(funcs);

            Register(_this,
                info => funcTable.Keys.Contains(info.Target.Parameter.Name),
                info => funcTable[info.Target.Parameter.Name](info.Target.Parameter.Name),
                info => info.Target.Parameter.Name ,
                lifestyle);
        }
        public static void RegisterByParameterNames<T1, T2>(this Container _this, Lifestyle lifestyle, Func<string, T1> func1, Func<string, T2> func2)
            where T1 : class
            where T2 : class
        {
            _this.RegisterByParameterNames(lifestyle, func1);
            _this.RegisterByParameterNames(lifestyle, func2);
        }

        public static void RegisterByParameterNames<T1, T2, T3>(this Container _this, Lifestyle lifestyle, Func<string, T1> func1, Func<string, T2> func2, Func<string, T3> func3)
            where T1 : class
            where T2 : class
            where T3 : class
        {
            _this.RegisterByParameterNames(lifestyle, func1);
            _this.RegisterByParameterNames(lifestyle, func2);
            _this.RegisterByParameterNames(lifestyle, func3);
        }

        public static void RegisterByParameterNames<T1, T2, T3, T4>(this Container _this, Lifestyle lifestyle, Func<string, T1> func1, Func<string, T2> func2, Func<string, T3> func3, Func<string, T4> func4)
            where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
        {
            _this.RegisterByParameterNames(lifestyle, func1);
            _this.RegisterByParameterNames(lifestyle, func2);
            _this.RegisterByParameterNames(lifestyle, func3);
            _this.RegisterByParameterNames(lifestyle, func4);
        }

        public static void RegisterByParameterNames<T1, T2, T3, T4, T5>(this Container _this, Lifestyle lifestyle, Func<string, T1> func1, Func<string, T2> func2, Func<string, T3> func3, Func<string, T4> func4, Func<string, T5> func5)
            where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
        {
            _this.RegisterByParameterNames(lifestyle, func1);
            _this.RegisterByParameterNames(lifestyle, func2);
            _this.RegisterByParameterNames(lifestyle, func3);
            _this.RegisterByParameterNames(lifestyle, func4);
            _this.RegisterByParameterNames(lifestyle, func5);
        }

        public static void RegisterByParameterNames<T1, T2, T3, T4, T5, T6>(this Container _this, Lifestyle lifestyle, Func<string, T1> func1, Func<string, T2> func2, Func<string, T3> func3, Func<string, T4> func4, Func<string, T5> func5, Func<string, T6> func6)
            where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
        {
            _this.RegisterByParameterNames(lifestyle, func1);
            _this.RegisterByParameterNames(lifestyle, func2);
            _this.RegisterByParameterNames(lifestyle, func3);
            _this.RegisterByParameterNames(lifestyle, func4);
            _this.RegisterByParameterNames(lifestyle, func5);
            _this.RegisterByParameterNames(lifestyle, func6);
        }
        public static void RegisterByParameterNames<T1, T2, T3, T4, T5, T6, T7>(this Container _this, Lifestyle lifestyle, Func<string, T1> func1, Func<string, T2> func2, Func<string, T3> func3, Func<string, T4> func4, Func<string, T5> func5, Func<string, T6> func6, Func<string, T7> func7)
            where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
        {
            _this.RegisterByParameterNames(lifestyle, func1);
            _this.RegisterByParameterNames(lifestyle, func2);
            _this.RegisterByParameterNames(lifestyle, func3);
            _this.RegisterByParameterNames(lifestyle, func4);
            _this.RegisterByParameterNames(lifestyle, func5);
            _this.RegisterByParameterNames(lifestyle, func6);
            _this.RegisterByParameterNames(lifestyle, func7);
        }

        public static void RegisterByParameterNames<T1, T2, T3, T4, T5, T6, T7, T8>(this Container _this, Lifestyle lifestyle, Func<string, T1> func1, Func<string, T2> func2, Func<string, T3> func3, Func<string, T4> func4, Func<string, T5> func5, Func<string, T6> func6, Func<string, T7> func7, Func<string, T8> func8)
            where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where T8 : class
        {
            _this.RegisterByParameterNames(lifestyle, func1);
            _this.RegisterByParameterNames(lifestyle, func2);
            _this.RegisterByParameterNames(lifestyle, func3);
            _this.RegisterByParameterNames(lifestyle, func4);
            _this.RegisterByParameterNames(lifestyle, func5);
            _this.RegisterByParameterNames(lifestyle, func6);
            _this.RegisterByParameterNames(lifestyle, func7);
            _this.RegisterByParameterNames(lifestyle, func8);
        }

        public static void RegisterByParameterNames<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Container _this, Lifestyle lifestyle, Func<string, T1> func1, Func<string, T2> func2, Func<string, T3> func3, Func<string, T4> func4, Func<string, T5> func5, Func<string, T6> func6, Func<string, T7> func7, Func<string, T8> func8, Func<string, T9> func9)
            where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where T8 : class
            where T9 : class
        {
            _this.RegisterByParameterNames(lifestyle, func1);
            _this.RegisterByParameterNames(lifestyle, func2);
            _this.RegisterByParameterNames(lifestyle, func3);
            _this.RegisterByParameterNames(lifestyle, func4);
            _this.RegisterByParameterNames(lifestyle, func5);
            _this.RegisterByParameterNames(lifestyle, func6);
            _this.RegisterByParameterNames(lifestyle, func7);
            _this.RegisterByParameterNames(lifestyle, func8);
            _this.RegisterByParameterNames(lifestyle, func9);
        }

        public static void RegisterByParameterNames<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Container _this, Lifestyle lifestyle, Func<string, T1> func1, Func<string, T2> func2, Func<string, T3> func3, Func<string, T4> func4, Func<string, T5> func5, Func<string, T6> func6, Func<string, T7> func7, Func<string, T8> func8, Func<string, T9> func9, Func<string, T10> func10)
            where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where T8 : class
            where T9 : class
            where T10 : class
        {
            _this.RegisterByParameterNames(lifestyle, func1);
            _this.RegisterByParameterNames(lifestyle, func2);
            _this.RegisterByParameterNames(lifestyle, func3);
            _this.RegisterByParameterNames(lifestyle, func4);
            _this.RegisterByParameterNames(lifestyle, func5);
            _this.RegisterByParameterNames(lifestyle, func6);
            _this.RegisterByParameterNames(lifestyle, func7);
            _this.RegisterByParameterNames(lifestyle, func8);
            _this.RegisterByParameterNames(lifestyle, func9);
            _this.RegisterByParameterNames(lifestyle, func10);
        }

        public static void RegisterByParameterNames<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this Container _this, Lifestyle lifestyle, Func<string, T1> func1, Func<string, T2> func2, Func<string, T3> func3, Func<string, T4> func4, Func<string, T5> func5, Func<string, T6> func6, Func<string, T7> func7, Func<string, T8> func8, Func<string, T9> func9, Func<string, T10> func10, Func<string, T11> func11)
            where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where T8 : class
            where T9 : class
            where T10 : class
            where T11 : class
        {
            _this.RegisterByParameterNames(lifestyle, func1);
            _this.RegisterByParameterNames(lifestyle, func2);
            _this.RegisterByParameterNames(lifestyle, func3);
            _this.RegisterByParameterNames(lifestyle, func4);
            _this.RegisterByParameterNames(lifestyle, func5);
            _this.RegisterByParameterNames(lifestyle, func6);
            _this.RegisterByParameterNames(lifestyle, func7);
            _this.RegisterByParameterNames(lifestyle, func8);
            _this.RegisterByParameterNames(lifestyle, func9);
            _this.RegisterByParameterNames(lifestyle, func10);
            _this.RegisterByParameterNames(lifestyle, func11);
        }


        static Dictionary<string, Func<string, T>> getNamedFuncs<T>(params Func<string, T>[] funcs)
        {
            return funcs
                .ToDictionary(f => f.Method.GetParameters().First().Name);
        }
    }
}
