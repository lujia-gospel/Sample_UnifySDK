namespace com.ultrasdk.unity.Entry
{
    public enum SharePlatform
    {
        ALL = 20000,//有界面
        QQ = 20001, //QQ好友
        QQ_ZONE = 20002,//QQ空间
        WECHAT = 20003,//微信好友
        WECHAT_MENTS = 20004,//微信朋友圈
        SINA = 20005 //新浪微博
    }
    public class UltraShareInfo
    {
        public bool hasUi; //true-有UI的分享，false-无UI的分享
        public string title;   //分享标题
        public string content;   //分享内容
        public string imgPath;   //分享图片本地地址
        public string imgUrl;   //分享图片网络地址
        public string url;   //分享链接
        public SharePlatform shareTo;   //分享到哪里
        public string extenal;   //额外备注
    }
}