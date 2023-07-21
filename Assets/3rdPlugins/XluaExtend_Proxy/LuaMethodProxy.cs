using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace LuaProxy
{
    public class LuaMethodProxy : ILuaProxy
    {
        private bool disposed = false;
        private LuaInvoker invoker;

        public LuaMethodProxy(object target, LuaFunction function)
        {
            if (target == null)
                throw new ArgumentNullException("target", "Unable to bind to target as it's null");
            this.invoker = new LuaInvoker(target, function);
        }

        public void Display()
        {
            Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (invoker != null && invoker is IDisposable)
                    {
                        (invoker as IDisposable).Dispose();
                        invoker = null;
                    }
                }
                disposed = true;
            }
        }
        public LuaInvoker GetInvoker()
        {
            return invoker;
        }
    }
}