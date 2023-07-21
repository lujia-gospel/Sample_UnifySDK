using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XLua;

namespace LuaProxy
{
    public class Binding : IDisposable
    {
        private ILuaProxy targetProxy;
        private EventHandler targetValueChangedHandler;

        public Binding(LuaTable table, LuaFunction fun, object target)
        {
            CreateTargetProxy(table, fun, target);
        }
        public void CreateTargetProxy(LuaTable table, LuaFunction fun, object target)
        {
            targetProxy = LuaProxyFactory.CreateProxy(table, fun);
            if (target is INotifiable && targetProxy != null)
            {
                targetValueChangedHandler = (parm, args) => UpdateSourceFromTarget(parm);
                (target as INotifiable).ValueChanged += targetValueChangedHandler;
            }
        }

        public void UpdateSourceFromTarget(params object[] parms)
        {
            try
            {
                // object[] values = (sender as IObtainable).GetValues();
                targetProxy.GetInvoker()?.Invoke(parms);
            }
            catch (Exception e)
            {

                Debug.LogWarning(e);
            }
        }
        public void Dispose()
        {
            if (targetProxy != null)
            {
                targetProxy.Display();
            }
        }

    }
}

