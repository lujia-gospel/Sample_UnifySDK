using System;

namespace com.ultrasdk.unity
{
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class UltraHMonoSingletonPath : Attribute
    {
        private string mPathInHierarchy;

        public UltraHMonoSingletonPath(string pathInHierarchy)
        {
            mPathInHierarchy = pathInHierarchy;
        }

        public string PathInHierarchy
        {
            get { return mPathInHierarchy; }
        }
    }
}