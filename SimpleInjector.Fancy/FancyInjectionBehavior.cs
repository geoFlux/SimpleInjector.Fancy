using System;
using SimpleInjector;
using SimpleInjector.Advanced;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace SimpleInjector.Fancy
{
    public class FancyInjectionBehavior<T> : IDependencyInjectionBehavior where T : class
    {
        readonly Container container;
        readonly IDependencyInjectionBehavior innerBehavior;
        readonly Func<InjectionConsumerInfo, bool> canResolve;
        readonly Func<InjectionConsumerInfo, T> resolve;
        readonly Lifestyle lifestyle;
        readonly Dictionary<object, Registration> registrations = new Dictionary<object, Registration>();
        readonly Func<InjectionConsumerInfo, object> keyFunc;
        public FancyInjectionBehavior(Container container, Func<InjectionConsumerInfo, bool> canResolve, Func<InjectionConsumerInfo, T> instanceCreator, Func<InjectionConsumerInfo, object> keyFunc, Lifestyle lifestyle)
        {
            this.container = container;
            this.innerBehavior = container.Options.DependencyInjectionBehavior;
            this.canResolve = canResolve;
            this.resolve = instanceCreator;
            this.lifestyle = lifestyle;
            if (keyFunc == null)
            {
                this.keyFunc = info => info;
            }
            else
            {
                this.keyFunc = keyFunc;
            }
        }
        public InstanceProducer GetInstanceProducer(InjectionConsumerInfo consumer, bool throwOnFailure)
        {
            if (this.canResolve(consumer))
            {
                var key = keyFunc(consumer);
                if (!registrations.ContainsKey(key))
                {
                    registrations[key] = lifestyle.CreateRegistration<T>(() => resolve(consumer), container);
                }
                var registration = registrations[key];
                return InstanceProducer.FromExpression(
                    typeof(T),
                    registration.BuildExpression(),
                    container);
            }
            return innerBehavior.GetInstanceProducer(consumer, throwOnFailure);
        }
        public void Verify(InjectionConsumerInfo consumer)
        {
            if (!this.canResolve(consumer))
            {
                innerBehavior.Verify(consumer);
            }
        }
    }


}