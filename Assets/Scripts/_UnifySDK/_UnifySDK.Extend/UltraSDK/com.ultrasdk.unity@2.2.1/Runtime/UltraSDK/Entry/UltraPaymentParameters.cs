namespace com.ultrasdk.unity.Entry
{
    public class UltraPaymentParameters
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        public string goodsId;
        /// <summary>
        /// 拓展字段
        /// </summary>
        public string extraParams;
        /// <summary>
        /// cp订单号
        /// </summary>
        public string cpOrder ;
        /// <summary>
        /// 支付回调地址
        /// </summary>
        public string callbackUrl ;
    }
}