using System;
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class UnifySDKEventAttribute: Attribute
{
    private Type type;
    public Type GetType()
    {
        return type;
    }

    public UnifySDKEventAttribute( Type _type)
    {
        type = _type;
    }
}