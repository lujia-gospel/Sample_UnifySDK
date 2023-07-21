using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public interface ILuaExtendable
{
    LuaTable GetMetatable();

    void SetMetatable(LuaTable table);

    void Display();
}
