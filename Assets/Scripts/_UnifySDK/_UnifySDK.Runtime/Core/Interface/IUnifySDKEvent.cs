using System;
using UnityEngine;

namespace UnifySDK.Event
{ 
    public interface IEvent
    {
        Type GetEventType();
    }
    public class AEvent<T> : IEvent where T : struct
    {
        public event EventHandler Handler;
        
        public Type GetEventType()
        {
            return typeof (T);
        }

        protected void Run(T t)
        {
            Handler?.Invoke(t,EventArgs.Empty);
        }

        public void Handle(T t)
        {
            try
            {
                Run(t);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}