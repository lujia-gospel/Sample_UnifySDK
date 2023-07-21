using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
namespace LuaProxy
{
    public class LuaInvoker : IDisposable
    {
        private readonly WeakReference target;
        private LuaFunction function;
        public object Target { get { return this.target != null && this.target.IsAlive ? this.target.Target : null; } }
        public LuaInvoker(object target, LuaFunction function)
        {
            if (target == null)
                throw new ArgumentNullException("target", "Unable to bind to target as it's null");
            this.target = new WeakReference(target, false);
            this.function = function;
        }

        public object Invoke(object[] args)
        {
            try
            {
                var target = this.Target;
                if (target == null)
                    return null;

                if (target is Behaviour behaviour && !behaviour.isActiveAndEnabled)
                    return null;

                int length = args != null ? args.Length + 1 : 1;
                object[] parameters = new object[length];
                parameters[0] = target;
                if (args != null && args.Length > 0)
                    Array.Copy(args, 0, parameters, 1, args.Length);

                return this.function.Call(parameters);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
            return null;
        }
        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (function != null)
                    {
                        function.Dispose();
                        function = null;
                    }
                }
                disposedValue = true;
            }
        }

        ~LuaInvoker()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
