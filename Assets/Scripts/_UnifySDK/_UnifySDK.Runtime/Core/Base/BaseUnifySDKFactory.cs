namespace UnifySDK
{
    public abstract class BaseUnifySDKFactory : IUnifySDKFactory
    {
        protected abstract string SDKName { get; }
        public virtual IUnifySDK Create()
        {
            return null;
        }
    }
}