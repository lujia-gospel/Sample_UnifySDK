using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

namespace UnifySDK.Event
{ 
    public interface IEvent
    {
        Type GetEventType();
        
        event EventHandler Handler;
    }
    public class AEvent<T> : IEvent where T : struct
    {
        public event EventHandler Handler;

        private List<EventHandler> _onceList = new List<EventHandler>();
        public void ClearEventHandler()
        {
            Handler = null;
        }

        public void AddOnceHandler(Action<T, EventArgs> action)
        {
            EventHandler temp= (data, eventArgs) => { action((T)data, eventArgs); };
            _onceList.Add(temp);
            Handler += temp;
        }

        public Type GetEventType()
        {
            return typeof (T);
        }

        protected void Run(T t)
        {
            UDebug.Sys.Log($"  EventSystem Run  Handler==null {(Handler==null).ToString()}");
            Handler?.Invoke(t,EventArgs.Empty);
            foreach (var v in _onceList)
            {
                Handler -= v;
            }
            _onceList.Clear();
        }

        public void Handle(T t)
        {
            try
            {
                Run(t);
            }
            catch (Exception e)
            {
                UDebug.Sys.LogError(e);
            }
        }
    }
}