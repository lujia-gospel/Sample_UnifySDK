using System;
using System.Collections.Generic;

namespace UnifySDK.Event
{ 
    public interface IEvent
    {
        Type GetEventType();
        
        event EventHandler Handler;
        void Handle(object t);
    }
    public class AEvent<T> : IEvent where T : struct
    {
        public event EventHandler Handler;
        public delegate void AEventHandler(T eventData);
        private List<EventHandler> _onceList = new List<EventHandler>();
        public void ClearEventHandler()
        {
            Handler = null;
        }

        public void AddHandler(AEventHandler action)
        {
            Handler+=(d,e)=> action((T)d);
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

        public void Handle(object t)
        {
            try
            {
                //UDebug.Sys.Log($"  EventSystem Run  Handler==null {(Handler==null).ToString()}");
                Handler?.Invoke(t,EventArgs.Empty);
                foreach (var v in _onceList)
                {
                    Handler -= v;
                }
                _onceList.Clear();
            }
            catch (Exception e)
            {
                UDebug.Sys.LogError(e);
            }
        }
    }
}