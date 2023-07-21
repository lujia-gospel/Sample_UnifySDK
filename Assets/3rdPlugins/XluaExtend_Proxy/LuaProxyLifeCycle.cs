using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace LuaProxy
{
    public class LuaProxyLifeCycle : MonoBehaviour
    {
        protected LuaTable metatable;//默认绑定的table
        protected List<Binding> m_BindingList;

        public void Bind(LuaTable table, LuaFunction fun, object target)
        {
            if (m_BindingList == null)
                m_BindingList = new List<Binding>();
            m_BindingList.Add(new Binding(table, fun, target));
        }
        
        public void Bind(LuaTable table, LuaFunction fun, EventHandler eventHandler)
        {
            if (m_BindingList == null)
                m_BindingList = new List<Binding>();
            m_BindingList.Add(new Binding(table, fun, new LuaEventHandler(eventHandler)));
        }

        public LuaTable GetMetatable()
        {
            return this.metatable;
        }
        public void SetMetatable(LuaTable table)
        {
            this.metatable = table;
        }

        public void Display()
        {
            foreach (var item in m_BindingList)
            {
                item.Dispose();
            }
            m_BindingList.Clear();
            metatable = null;
        }
        protected virtual void OnDestroy()
        {
            Display();
        }
    }
}
