#define XLUA
#if XLUA
using LuaProxy;
using UnifySDK.Event;
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
        public void InitLuaProxy(LuaTable defaultTable)
        {
            if (m_LuaLifeCycle == null)
                m_LuaLifeCycle= new GameObject("UnifySDK_LuaProxyLifeCycle").AddComponent<LuaProxyLifeCycle>();
            GameObject.DontDestroyOnLoad(m_LuaLifeCycle.gameObject);
            m_LuaLifeCycle.SetMetatable(defaultTable);
        }

        /// <summary>
        /// 绑定aEvent
        /// </summary>
        /// <param name="fun">LuaFunction</param>
        /// <param name="aEvent"></param>
        public void Bind(LuaFunction fun, object aEvent) 
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
            LuaEventHandler luaEventHandler = new LuaEventHandler();
            iEvent.Handler += (eventData,eventArgs) =>
            {
                luaEventHandler.SendValueChanged(eventData,eventArgs);
            };
            m_LuaLifeCycle.Bind(table, fun, luaEventHandler);
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
            LuaEventHandler luaEventHandler = new LuaEventHandler();
            iEvent.Handler += (eventData,eventArgs) => { luaEventHandler.SendValueChanged(eventData,eventArgs); };
            m_LuaLifeCycle.Bind(table, fun, luaEventHandler);
        }
      
        #endregion
    }
}
#endif