namespace com.ultrasdk.unity.Entry
{
    public class UltraPaymentOrder
    {
        public string plat;
        public string errorMsg ;
        public string sdkOrderId;

        /*
         * iOS独有
         * **/
        public string orderAmount ;
        public string currency ;

        /*
         * Android独有
         * **/
        public string cpOrderId;
        public string extraParams;
        
    }
}