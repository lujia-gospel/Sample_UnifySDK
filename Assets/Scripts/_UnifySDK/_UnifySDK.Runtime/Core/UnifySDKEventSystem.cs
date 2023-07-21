using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace UnifySDK.Event
{
    public class UnifySDKEventSystem
    {
        
        private static UnifySDKEventSystem instance;

        public static UnifySDKEventSystem Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UnifySDKEventSystem();
                }

                return instance;
            }
        }
        private readonly Dictionary<Type, List<object>> allEvents = new Dictionary<Type, List<object>>();
        public void UnifySDKInitEvent(object unifySDK)
        {
            var type = unifySDK.GetType();
            PropertyInfo[] propertyInfos = type.GetProperties();
            foreach (var property in propertyInfos)
            {
                var attrs = property.GetCustomAttributes(true);
                foreach (var attr in attrs)
                {
                    if (attr is UnifySDKEventAttribute)
                    {
                        Type genericType = (attr as UnifySDKEventAttribute).GetType();
                        object obj = Activator.CreateInstance(genericType);
                        IEvent iEvent =  obj as IEvent;
                        property.SetValue(unifySDK,obj);
                        if (allEvents.ContainsKey(iEvent.GetEventType()))
                            allEvents[iEvent.GetEventType()].Add(iEvent);
                        else
                            allEvents.Add(iEvent.GetEventType(),new List<object>(){iEvent});
                        break;
                    }
                }
            }
        }
        public void Publish<T>(T a) where T : struct
        {
            List<object> iEvents;
            if (!this.allEvents.TryGetValue(a.GetType(), out iEvents))
            {
                return;
            }
            
            for (int i = 0; i < iEvents.Count; ++i)
            {
                object obj = iEvents[i];
                if (!(obj is AEvent<T> aEvent))
                {
                    Debug.LogError($"event error: {obj.GetType().Name}");
                    continue;
                }
                aEvent.Handle(a);
            }
        }
    }
}