using System;
using System.Reflection;

namespace com.ultrasdk.unity
{
    public class UltraSingletonCreator
    {
        public static T CreateSingleton<T>() where T : class, UltraISingleton
        {
            var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);

            // Get No-argument constructor
            var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);

            if (ctor == null)
            {
                throw new Exception("Non-Public Constructor() not found! in " + typeof(T));
            }

            // Through the constructor, common examples
            var retInstance = ctor.Invoke(null) as T;
            retInstance.OnSingletonInit();

            return retInstance;
        }
    }
}