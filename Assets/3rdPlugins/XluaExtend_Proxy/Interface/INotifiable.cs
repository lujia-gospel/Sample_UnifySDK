using System;

namespace LuaProxy
{
    public interface INotifiable
    {
        event EventHandler ValueChanged;
    }
}



