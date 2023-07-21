using System;
using LuaProxy;
public class LuaEventHandler:INotifiable
{
    public event EventHandler ValueChanged;
    public void SendValueChanged(object args,EventArgs eventArgs)
    {
        ValueChanged?.Invoke(args,eventArgs);
    }

    public LuaEventHandler(){}

    public LuaEventHandler(EventHandler e)
    {
        ValueChanged = e;
    }
    
}
