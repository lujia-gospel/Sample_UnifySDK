using System;
using UnifySDK.Event;
using UnifySDK.Event.Purchase;

namespace UnifySDK
{
    [UnifySDKInterface]
    public interface IUnifySDK_Purchase
    {
        void QueryGoods();
        
        void Purchase(PurchaseOrderInfoData data);
        
        void SubmitPurchaseRoleInfo(PurchaseRoleInfoData data);

        /// <summary>
        /// 消耗订单 ---目前只有Google
        /// </summary>
        void ConsumePurchase(string token);

        /// <summary>
        /// 补单 ---目前只有Google
        /// </summary>
        void QueryPurchasesAsync();
        
        #region Listener
        [UnifySDKEvent(typeof(AEvent<PurchaseSuccessData>))]
        public AEvent<PurchaseSuccessData> OnPurchaseSuccess { get; set; }
        
        [UnifySDKEvent(typeof(AEvent<PurchaseFailedData>))]
        public AEvent<PurchaseFailedData> OnPurchaseFailed { get; set; }
        
        [UnifySDKEvent(typeof(AEvent<PurchaseCancelData>))]
        public AEvent<PurchaseCancelData> OnPurchaseCancel { get; set; }
        
        [UnifySDKEvent(typeof(AEvent<ConsumeSucceedData>))]
        public AEvent<ConsumeSucceedData> OnConsumeSuccess { get; set; }
        
        [UnifySDKEvent(typeof(AEvent<RePaySuccessData>))]
        public AEvent<RePaySuccessData> OnRePaySuccess { get; set; }
        
        #endregion
    }
}
