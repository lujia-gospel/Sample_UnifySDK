using System;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class UnifySDKEventDataAttribute : Attribute
{
    private string describe;

    public string GetDes()
    {
        return describe;
    }
    
    public UnifySDKEventDataAttribute()
    {
        
    }

    public UnifySDKEventDataAttribute(string _describe)
    {
        describe = _describe;
    }
}