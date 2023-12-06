using UnityEngine;

namespace com.ultrasdk.unity
{
    public class UltraMonoSingleton <T> : MonoBehaviour, UltraISingleton where T : UltraMonoSingleton<T>
    {
        protected static T mInstance;

        public static T Instance
        {
            get
            {
                if (mInstance == null && !mOnApplicationQuit)
                {
                    mInstance = UltraMonoSingletonCreator.CreateMonoSingleton<T>();
                }

                return mInstance;
            }
        }


        public static bool IsApplicationQuit
        {
            get { return mOnApplicationQuit; }
        }

        public virtual void OnSingletonInit()
        {
        }

        public virtual void Dispose()
        {
            // Debug.Log($"MonoSingleton Lifecycle {GetType().Name} : Dispose");
            if (UltraMonoSingletonCreator.IsUnitTestMode)
            {
                var curTrans = transform;
                do
                {
                    var parent = curTrans.parent;
                    DestroyImmediate(curTrans.gameObject);
                    curTrans = parent;
                } while (curTrans != null);

                mInstance = null;
            }
            else
            {
                OnSingletonDestroy();
                Destroy(gameObject);
            }
        }

        protected static bool mOnApplicationQuit = false;

        protected virtual void OnSingletonDestroy()
        {
            // Debug.Log($"MonoSingleton Lifecycle {GetType().Name} : OnSingletonDestroy");
        }

        private void OnDestroy()
        {
            // Debug.Log($"MonoSingleton Lifecycle {GetType().Name} : OnDestroy");
            // OnSingletonDestroy();
            mInstance = null;
        }

        protected virtual void OnApplicationQuit()
        {
            // Debug.Log($"MonoSingleton Lifecycle {GetType().Name} : OnApplicationQuit");
            mOnApplicationQuit = true;
            if (mInstance == null) return;
            Destroy(mInstance.gameObject);
            mInstance = null;
        }
    }
}