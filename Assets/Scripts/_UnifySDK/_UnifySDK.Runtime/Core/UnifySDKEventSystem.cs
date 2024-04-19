using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace UnifySDK.Event
{
    public class UnifySDKEventSystem
    {
        private static UnifySDKEventSystem instance;
        public static UnifySDKEventSystem Instance => instance ??= new UnifySDKEventSystem();

        private readonly Dictionary<UnifySDKType, Dictionary<Type, IEvent>> allEvents = new();

        public void UnifySDKInitEvent(UnifySDKType sdkType, object unifySDK)
        {
            var type = unifySDK.GetType();
            var interfaceTypes = type.GetInterfaces();
            var eventDictionary = new Dictionary<Type, IEvent>();
            allEvents[sdkType] = eventDictionary;

            foreach (var interfaceType in interfaceTypes)
            {
                var propertyInfos = interfaceType.GetProperties();
                foreach (var property in propertyInfos)
                {
                    InitializeEventForProperty(unifySDK, property, eventDictionary);
                }
            }
        }

        private void InitializeEventForProperty(object unifySDK, PropertyInfo property, Dictionary<Type, IEvent> eventDictionary)
        {
            var attrs = property.GetCustomAttributes(typeof(UnifySDKEventAttribute), true);
            foreach (UnifySDKEventAttribute attr in attrs)
            {
                if (Activator.CreateInstance(attr.GetType()) is IEvent iEvent)
                {
                    property.SetValue(unifySDK, iEvent);
                    eventDictionary[iEvent.GetEventType()] = iEvent;
                }
                else
                {
                    UDebug.Sys.LogError($"{attr.GetType().Name} 必须是继承自 IEvent");
                }
            }
        }

        public void Publish(object a, UnifySDKType type = UnifySDKType.All)
        {
            var eventType = a.GetType();
            foreach (var kv in allEvents)
            {
                if (type == UnifySDKType.All || (kv.Key & type) == kv.Key)
                {
                    if (kv.Value.TryGetValue(eventType, out var iEvent))
                    {
                        iEvent.Handle(a);
                    }
                }
            }
        }
    }
}