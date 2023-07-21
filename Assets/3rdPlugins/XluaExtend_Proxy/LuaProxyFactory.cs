using XLua;

namespace LuaProxy
{
    public class LuaProxyFactory
    {
        public static ILuaProxy CreateProxy(LuaTable table, string targetName)
        {
            if (table != null)
            {
                var obj = table.Get<object>(targetName);
                if (obj != null)
                {
                    LuaFunction fun = obj as LuaFunction;
                    if (fun != null)
                        return new LuaMethodProxy(table, fun);
                }
            }
            return null;
        }
        public static ILuaProxy CreateProxy(LuaTable table, LuaFunction fun)
        {
            if (table != null && fun != null)
                return new LuaMethodProxy(table, fun);
            return null;
        }
    }
}
