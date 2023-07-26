using System;
using UnifySDK.Event;
namespace UnifySDK
{
    [UnifySDKInterface]
    public interface IUnifySDK_Purchase
    {
        void QueryGoods();
        
        void Purchase(PurchaseOrderInfo data);
        
        void SubmitPurchaseRoleInfo(PurchaseRoleInfo data);
        
        #region Listener
        public AEvent<PurchaseSuccessData> OnPurchaseSuccess { get; set; }
        
        public AEvent<PurchaseFailedData> OnPurchaseFailed { get; set; }
        
        public AEvent<PurchaseCancelData> OnPurchaseCancel { get; set; }
        
        #endregion
    }
}
