#define XLUA
#if XLUA
using System.Reflection;
using LuaProxy;
using UnifySDK.Event;
using UnityEditor;
using UnityEngine;
using XLua;
namespace UnifySDK
{
    public partial class UnifySDKManager
    {
        #region Lua绑定Hander  
        private LuaProxyLifeCycle m_LuaLifeCycle;
    
        public LuaProxyLifeCycle LuaLifeCycle => m_LuaLifeCycle;
    
        /// <summary>
        /// 初始LuaProxy
        /// </summary>
        /// <param name="defaultTable">默认table</param>
        /// <param name="luaLifeCycle"></param>
        public void InitLuaProxy(LuaTable defaultTable, LuaProxyLifeCycle luaLifeCycle)
        {
            if (m_LuaLifeCycle == null)
                m_LuaLifeCycle= new GameObject("UnifySDK_LuaProxyLifeCycle").AddComponent<LuaProxyLifeCycle>();
            m_LuaLifeCycle = luaLifeCycle;
            m_LuaLifeCycle.SetMetatable(defaultTable);
        }

        /// <summary>
        /// 绑定aEvent
        /// </summary>
        /// <param name="fun">LuaFunction</param>
        /// <param name="aEvent"></param>
        public  void Bind(LuaFunction fun, object aEvent) 
        {
            if (m_LuaLifeCycle == null)
                return;
            IEvent iEvent = aEvent as IEvent;
            if (iEvent==null)
            {
                UDebug.Sys.LogError($"error aEvent is not IEvent");
                return;
            }
            LuaTable table = m_LuaLifeCycle.GetMetatable();
            MethodInfo genericMethodBind = typeof(UnifySDKManager).GetMethod("GenericMethodBind").MakeGenericMethod( iEvent.GetEventType() );
            genericMethodBind.Invoke(null,new []{table,fun,aEvent} );
        }

        /// <summary>
        /// 绑定aEvent
        /// </summary>
        /// <param name="fun">LuaFunction</param>
        /// <param name="aEvent"></param>
        public void Bind(LuaTable table,LuaFunction fun, object aEvent) 
        {
            if (m_LuaLifeCycle == null)
                return;
            IEvent iEvent = aEvent as IEvent;
            if (iEvent==null)
            {
                UDebug.Sys.LogError($"error aEvent is not IEvent");
                return;
            }
            MethodInfo genericMethodBind = typeof(UnifySDKManager).GetMethod("GenericMethodBind").MakeGenericMethod(iEvent.GetEventType());
            genericMethodBind.Invoke(null,new []{table,fun,aEvent});
        }
        
        /// <summary>
        /// 绑定aEvent
        /// </summary>
        /// <param name="table">LuaTabel</param>
        /// <param name="fun">LuaFunction</param>
        /// <param name="aEvent"></param>
        public static void GenericMethodBind<T>(LuaTable table, LuaFunction fun, AEvent<T> aEvent)where T:struct
        {
            LuaEventHandler luaEventHandler = new LuaEventHandler();
            aEvent.Handler += (eventData,eventArgs) => { luaEventHandler.SendValueChanged(eventData,eventArgs); };
            Instance.m_LuaLifeCycle.Bind(table, fun, luaEventHandler);
        }
        #endregion
    }
}
#endif