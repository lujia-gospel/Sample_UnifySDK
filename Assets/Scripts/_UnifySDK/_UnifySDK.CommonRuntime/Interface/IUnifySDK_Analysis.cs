using UnifySDK.Event.Analysis;

namespace UnifySDK
{
    [UnifySDKInterface]
    public interface IUnifySDK_Analysis
    {
        /// <summary>
        /// 玩家服务器注册
        /// </summary>
        void Register(RegisterData data);

        /// <summary>
        /// 玩家的账号登陆服务器
        /// </summary>
        void Login(LoginData data);

        /// <summary>
        /// 玩家的充值数据
        /// </summary>
        void Recharge(RechargeData data);

        /// <summary>
        /// 玩家的订单数据
        /// </summary>
        void Order(OrderData data);


        /// <summary>
        /// 统计玩家的自定义事件
        /// </summary>
        void SetCustomEvent(CustomEventData data);

        /// <summary>
        /// 设置log开关
        /// </summary>
        /// <param name="print"></param>
        void SetPrintLog(bool print);

        /// <summary>
        /// 监测页面展示时长
        /// </summary>
        void SetTrackViewDuration(TrackViewDurationData data);

        /// <summary>
        /// 广告展示时调用
        /// </summary>
        void SetTrackAdShow(TrackAdShowData data);

        /// <summary>
        /// 广告点击时调用
        /// </summary>
        void SetTrackAdClick(TrackAdClickData data);

        /// <summary>
        /// 统计APP运行时长
        /// </summary>
        void SetTrackAppDuration(TrackAppDurationData data);

        /// <summary>
        ///  获取广告归因
        /// </summary>
        string getAttribution();
    }
}