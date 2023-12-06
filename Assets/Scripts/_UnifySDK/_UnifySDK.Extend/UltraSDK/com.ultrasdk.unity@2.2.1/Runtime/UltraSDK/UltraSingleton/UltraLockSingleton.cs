using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultrasdk.unity
{
    public abstract class UltraLockSingleton<T> : UltraISingleton where T : UltraLockSingleton<T>
    {
        protected static T mInstance;

        static object mLock = new object();

        public static T Instance
        {
            get
            {
                lock (mLock)
                {
                    if (mInstance == null)
                    {
                        mInstance = UltraSingletonCreator.CreateSingleton<T>();
                    }
                }

                return mInstance;
            }
        }

        public virtual void Dispose()
        {
            OnSingletonDestroy();
            mInstance = null;
        }

        public virtual void OnSingletonInit()
        {
        }

        public virtual void OnStart()
        {
        }

        protected virtual void OnSingletonDestroy()
        {
        }
    }
}
