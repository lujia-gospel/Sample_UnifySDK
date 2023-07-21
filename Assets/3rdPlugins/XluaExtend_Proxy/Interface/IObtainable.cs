using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LuaProxy
{
    public interface IObtainable
    {
        object[] GetValues();

        //TValue GetValue<TValue>();
    }
}
