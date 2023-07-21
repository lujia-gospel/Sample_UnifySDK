using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace LuaProxy
{
    public interface ILuaProxy
    {
        void Display();
        LuaInvoker GetInvoker();
    }
}